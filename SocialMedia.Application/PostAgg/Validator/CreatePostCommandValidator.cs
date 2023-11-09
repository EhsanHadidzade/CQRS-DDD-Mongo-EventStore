using FluentValidation;
using SocialMedia.Application.PostAgg.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.PostAgg.Validator
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(command => command.title)
        .NotEmpty()
        .MaximumLength(64);

            RuleFor(command => command.Description)
                .NotEmpty()
                .MaximumLength(512);
        }
    }
}
