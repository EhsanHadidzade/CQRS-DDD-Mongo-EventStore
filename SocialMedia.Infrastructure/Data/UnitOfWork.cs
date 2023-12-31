using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMedia.Core.Abstractions;
using SocialMedia.Core.Events;
using SocialMedia.Core.Extensions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data.Context;

namespace SocialMedia.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly WriteDbContext _writeDbContext;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        WriteDbContext writeDbContext,
        IEventStoreRepository eventStoreRepository,
        IMediator mediator,
        ILogger<UnitOfWork> logger)
    {
        _writeDbContext = writeDbContext;
        _eventStoreRepository = eventStoreRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task SaveChangesAsync()
    {
        // Criando a estratégia de execução (Connection resiliency and database retries).
        var strategy = _writeDbContext.Database.CreateExecutionStrategy();

        // Executando a estratégia.
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction
                = await _writeDbContext.Database.BeginTransactionAsync();

            _logger.LogInformation("----- Begin transaction: '{TransactionId}'", transaction.TransactionId);

            try
            {
                // Obtendo os eventos e stores das entidades rastreadas no contexto do EF Core.
                var (domainEvents, eventStores) = BeforeSaveChanges();

                var rowsAffected = await _writeDbContext.SaveChangesAsync();

                _logger.LogInformation("----- Commit transaction: '{TransactionId}'", transaction.TransactionId);

                await transaction.CommitAsync();

                // Disparando os eventos e salvando os stores.
                await AfterSaveChangesAsync(domainEvents, eventStores);

                _logger.LogInformation(
                    "----- Transaction successfully confirmed: '{TransactionId}', Rows Affected: {RowsAffected}",
                    transaction.TransactionId, rowsAffected);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected exception occurred while committing the transaction: '{TransactionId}', message: {Message}",
                    transaction.TransactionId, ex.Message);

                await transaction.RollbackAsync();

                throw;
            }
        });
    }

    private (IEnumerable<IDomainEvent> domainEvents, IEnumerable<EventStore> eventStores) BeforeSaveChanges()
    {
        var domainEntities = _writeDbContext
            .ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = new List<IDomainEvent>();
        var eventStores = new List<EventStore>();

        if (domainEntities.Any())
        {
            domainEvents = domainEntities
                .SelectMany(entry => entry.Entity.DomainEvents)
                .ToList();

            foreach (var @event in domainEvents)
            {
                var type = @event.GetGenericTypeName();
                var data = @event.ToJson();
                eventStores.Add(new EventStore(type, data));
            }

            // Limpando os eventos das entidades.
            domainEntities
                .ForEach(entry => entry.Entity.ClearDomainEvents());
        }

        return (domainEvents, eventStores);
    }

    private async Task AfterSaveChangesAsync(IEnumerable<IDomainEvent> domainEvents, IEnumerable<EventStore> eventStores)
    {
        if (domainEvents.Any() && eventStores.Any())
        {
            // Agrupando todos os eventos em uma lista de Task's.
            var tasks = domainEvents
                .Select((@event) => _mediator.Publish(@event));

            // Disparando as notificações.
            await Task.WhenAll(tasks);

            // Salvando os eventos.
            await _eventStoreRepository.InsertManyAsync(eventStores);
        }
    }
}