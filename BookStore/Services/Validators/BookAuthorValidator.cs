using BookStore.Controllers.RequestModels;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Validators
{
    public class BookAuthorValidator : AbstractValidator<BookAuthorsRequest>
    {
        private readonly BookStoreContext dbContext;

        public BookAuthorValidator(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(dbContext));

            RuleFor(ba => ba.BookId).Must(BookExists).WithMessage("Book not found");
            RuleForEach(ba => ba.AuthorsCollection).Must(AuthorExists).WithMessage("Author not found");
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

        /// <summary>
        /// Check author is in the database.
        /// </summary>
        /// <param name="id">Author id</param>
        /// <returns>success</returns>
        private bool AuthorExists(Guid id)
        {
            return dbContext.Authors.Any(a => a.Id == id);
        }
    }
}
