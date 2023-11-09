using System;
using MediatR;

namespace SocialMedia.Core.Interfaces;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}