using SocialMedia.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.QueriesModel
{
    public class PostQueryModel:BaseQueryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URLAddress { get; set; }
    }
}
