using SocialMedia.Core.Abstractions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Domain.Entities.PostAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace SocialMedia.Domain.Entities.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string URLAddress { get; private set; }

        private Post()
        {

        }

        public Post(string title, string description, string urlAddress)
        {
            Title = title;
            Description = description;
            URLAddress = urlAddress;

            //
            AddDomainEvent(new PostCreatedEvent(Id, Title, Description, urlAddress));
        }

        public void Edit(string title, string description, string urlAddress)
        {
            Title = title;
            Description = description;
            URLAddress = urlAddress;

            //
            AddDomainEvent(new PostEditedEvent(Id, Title, Description, urlAddress));
        }


        public void Delete() => AddDomainEvent(new PostDeletedEvent(Id, Title, Description, URLAddress));


    }
}
