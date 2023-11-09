using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Abstractions;

public abstract class BaseEntity : IEntityKey<Guid>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; private init; } = Guid.NewGuid();

    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}