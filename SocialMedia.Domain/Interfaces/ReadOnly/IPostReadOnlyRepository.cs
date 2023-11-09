using SocialMedia.Core.Interfaces;
using SocialMedia.Domain.QueriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces.ReadOnly
{
    public interface IPostReadOnlyRepository : IReadOnlyRepository<PostQueryModel>
    {
        Task<PostQueryModel> GetByTitleAsync(string title);
        Task<IEnumerable<PostQueryModel>> GetAllAsync();


    }
}
