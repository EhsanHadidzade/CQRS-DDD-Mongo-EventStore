using SocialMedia.Domain.Entities.PostAggregate;
using SocialMedia.Domain.Interfaces.WriteOnly;
using SocialMedia.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Data.Repositories.WriteOnly
{
    public class PostWriteOnlyRepository : BaseWriteOnlyRepository<Post>, IPostWriteOnlyRepository
    {
        public PostWriteOnlyRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
