using BookStore.Controllers.RequestModels;
using BookStore.DataAccess;
using FluentValidation;
using System;
using System.Linq;

namespace BookStore.Services.Validators
{
    public class DiscountValidator : AbstractValidator<DiscountRequest>
    {
        private readonly BookStoreContext dbContext;

        public DiscountValidator(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(dbContext));
            
            RuleForEach(d => d.BooksId).Must(BookExists).WithMessage("Book not found");
        }

        /// <summary>
        /// Check book is in the database.
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>success</returns>
        private bool BookExists(Guid id)
        {
            return dbContext.Books.Any(b => b.Id == id);
        }
    }
}
