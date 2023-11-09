using SocialMedia.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities.PostAggregate.Events
{
    public abstract class PostBaseEvent : BaseDomainEvent
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string URLAddress { get; private init; }
        public Guid Id { get; private init; }

        protected PostBaseEvent(Guid id, string title, string description, string urlAddress)
        {
            Id = id;
            Title = title;
            Description = description;
            URLAddress = urlAddress;
        }
    }
}
