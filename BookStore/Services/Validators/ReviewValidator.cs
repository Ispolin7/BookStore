using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace BookStore.Services.Validators
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        private readonly BookStoreContext dbContext;

        public ReviewValidator(BookStoreContext context)
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
