using SocialMedia.Core.Interfaces;
using System;

namespace SocialMedia.Core.Abstractions;

public abstract class BaseDomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; private init; } = DateTime.Now;
}