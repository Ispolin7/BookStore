using BookStore.DataAccess.Models;
using FluentValidation;
using System;

namespace BookStore.Services.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.ActualPrice).LessThanOrEqualTo(b => b.OrgPrice).WithMessage("ActualPrice can not be more than OrgPrice");
            RuleFor(b => b.PublishedOn).LessThan(DateTime.Now).WithMessage("PublishedOn can not be more than curent date");
        }
    }
}
