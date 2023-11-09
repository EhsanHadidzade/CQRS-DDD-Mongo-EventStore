using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SocialMedia.Core.Interfaces;
using SocialMedia.Domain.Entities.PostAggregate.Events;
using SocialMedia.Domain.QueriesModel;

namespace SocialMedia.Application.PostAgg.Event;

/// <summary>
/// Manipulador de Eventos do Cliente.
/// </summary>
public class PostEventHandler :
    INotificationHandler<PostCreatedEvent>,
    INotificationHandler<PostEditedEvent>,
    INotificationHandler<PostDeletedEvent>
{
    private readonly IMapper _mapper;
    private readonly ISyncDataBase _syncDataBase;

    public PostEventHandler(IMapper mapper, ISyncDataBase syncDataBase)
    {
        _mapper = mapper;
        _syncDataBase = syncDataBase;
    }

    public async Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
        => await SaveAsync(_mapper.Map<PostQueryModel>(notification));

    public async Task Handle(PostEditedEvent notification, CancellationToken cancellationToken)
        => await SaveAsync(_mapper.Map<PostQueryModel>(notification));

    public async Task Handle(PostDeletedEvent notification, CancellationToken cancellationToken)
        => await _syncDataBase.DeleteAsync<PostQueryModel>(filter => filter.Id == notification.Id);

    private async Task SaveAsync(PostQueryModel queryModel)
        => await _syncDataBase.SaveAsync(queryModel, filter => filter.Id == queryModel.Id);
}