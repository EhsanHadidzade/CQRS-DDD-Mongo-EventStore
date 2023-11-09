using System;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Abstractions;

public abstract class BaseQueryModel : IQueryModel
{
    public Guid Id { get; private init; } = Guid.NewGuid();
}