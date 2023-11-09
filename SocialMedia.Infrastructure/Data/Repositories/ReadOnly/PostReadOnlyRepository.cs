using MongoDB.Driver;
using SocialMedia.Domain.Interfaces.ReadOnly;
using SocialMedia.Domain.QueriesModel;
using SocialMedia.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Data.Repositories.ReadOnly
{
    public class PostReadOnlyRepository : BaseReadOnlyRepository<PostQueryModel>, IPostReadOnlyRepository
    {
        public PostReadOnlyRepository(ReadDbContext readDbContext) : base(readDbContext)
        {
        }

        public async Task<IEnumerable<PostQueryModel>> GetAllAsync()
        {
            var filter = Builders<PostQueryModel>.Filter.Empty;
            return await Collection
                .Find(filter)
                .ToListAsync();
        }

        public Task<PostQueryModel> GetByTitleAsync(string title)
            => Collection.Find(query => query.Title == title).FirstOrDefaultAsync();

    }
}
