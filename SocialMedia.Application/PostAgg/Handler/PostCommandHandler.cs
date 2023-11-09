using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SocialMedia.Application.PostAgg.Command;
using SocialMedia.Application.PostAgg.Response;
using SocialMedia.Application.PostAgg.Validator;
using SocialMedia.Core.Interfaces;
using SocialMedia.Domain.Entities.PostAggregate;
using SocialMedia.Domain.Interfaces.ReadOnly;
using SocialMedia.Domain.Interfaces.WriteOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.PostAgg.Handler
{
    public class PostCommandHandler :
        IRequestHandler<CreatePostCommand, Result<CreatedPostResponse>>
    {

        private readonly CreatePostCommandValidator _createPostCommandValidator;
        private readonly IPostWriteOnlyRepository _writeOnlyRepository;
        private readonly IPostReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public PostCommandHandler
            (
            CreatePostCommandValidator createPostCommandValidator,
            IPostWriteOnlyRepository writeOnlyRepository,
            IPostReadOnlyRepository readOnlyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _createPostCommandValidator = createPostCommandValidator;
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<CreatedPostResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _createPostCommandValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Invalid(validationResult.AsErrors());

            var postURL = await GetPostURL(request.File);
            var post = new Post(request.title, request.Description, postURL);

            _writeOnlyRepository.Add(post);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(new CreatedPostResponse(post.Id));
        }


        private async Task<string> GetPostURL(string file)
        {
            //Upload Image to S3 or other storages and return url
            return $"https://{Guid.NewGuid()}";
        }
    }
}
