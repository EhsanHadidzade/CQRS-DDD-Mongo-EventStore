using Shop.Core.Interfaces;
using SocialMedia.Domain.Entities.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces.WriteOnly
{
    public interface IPostWriteOnlyRepository : IWriteOnlyRepository<Post>
    {
    }
}
