using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities.PostAggregate.Events
{
    public class PostEditedEvent : PostBaseEvent
    {
        public PostEditedEvent(Guid id, string title, string description,string urlAddress) : base(id, title, description,urlAddress)
        {

        }
    }
}
