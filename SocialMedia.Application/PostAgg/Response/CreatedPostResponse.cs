using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.PostAgg.Response
{
    public class CreatedPostResponse
    {
        public CreatedPostResponse(Guid id) => Id = id;

        public Guid Id { get; }
    }
}
