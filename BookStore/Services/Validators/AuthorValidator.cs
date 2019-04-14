using BookStore.DataAccess.Models;
using FluentValidation;

namespace BookStore.Services.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Please specify a name");
        }
    }
}
