using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Validators
{
    public class LineItemValidator : AbstractValidator<LineItem>
    {
        private readonly BookStoreContext dbContext;

        public LineItemValidator(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(dbContext));
            RuleFor(r => r.BookId).Must(BookExists).WithMessage("Book not found");
        }

        /// <summary>
        /// Check the presence of an entity in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BookExists(Guid id)
        {
            return dbContext.Books.Any(b => b.Id == id);
        }
    }
}
