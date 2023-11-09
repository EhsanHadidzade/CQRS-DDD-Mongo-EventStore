using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using SocialMedia.Application.PostAgg.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace SocialMedia.Application.PostAgg.Command
{
    public class CreatePostCommand : IRequest<Result<CreatedPostResponse>>
    {


        //[Required]
        //[MaxLength(64)]
        //[DataType(DataType.Text)]
        public string title { get; set; }

        //[Required]
        //[MaxLength(512 )]
        //[DataType(DataType.Text)]
        public string Description { get; set; }

        //[Required]
        public string File { get; set; }


    }
}
