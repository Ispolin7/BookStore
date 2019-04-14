using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using FluentValidation;

namespace BookStore.Services.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator(BookStoreContext dbContext)
        {
            RuleForEach(o => o.LineItems).SetValidator(new LineItemValidator(dbContext));
        }
    }
}
