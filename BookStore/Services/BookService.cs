using BookStore.Common;
using BookStore.Controllers.RequestModels;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly BookStoreContext dbContext;
        private readonly IValidator<Book> bookValidator;
        private readonly IValidator<BookAuthorsRequest> bookAuthorsValidator;
        private readonly IValidator<DiscountRequest> discountValidator;

        public BookService(
            BookStoreContext context, 
            IValidator<Book> bookValidator, 
            IValidator<BookAuthorsRequest> bookAuthorsValidator,
            IValidator<DiscountRequest> discountValidator
            )
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.bookValidator = bookValidator ?? throw new ArgumentNullException(nameof(bookValidator));
            this.bookAuthorsValidator = bookAuthorsValidator ?? throw new ArgumentNullException(nameof(bookAuthorsValidator));
            this.discountValidator = discountValidator ?? throw new ArgumentNullException(nameof(discountValidator));
        }

        /// <summary>
        /// Get all not deleted models from DB.
        /// </summary>
        /// <returns>models collection</returns>
        public async Task<IEnumerable<Book>> AllAsync()
        {
            var collection = await this.dbContext.Books
                .AsNoTracking()
                .Where(b => b.SoftDeleted == false)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();

            collection.ForEach(b => b.Authors = this.SortAutors(b.BookAuthors));
            return collection;
        }

        /// <summary>
        /// Get model's information from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>model's information</returns>
        public async Task<Book> GetAsync(Guid id)
        {
            var book = await this.dbContext.Books
                .AsNoTracking()
                .Where(b => (b.SoftDeleted == false) && (b.Id == id))
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Reviews)
                .FirstAsync();


            book.Authors = this.SortAutors(book.BookAuthors);
            return book;
        }

        /// <summary>
        /// Add new model to DB.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>model's id</returns>
        public async Task<Guid> SaveAsync(Book book)
        {
            this.bookValidator.Validate(book).ThrowIfInvalid();
            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();
            return book.Id;
        }        

        /// <summary>
        /// Change softDelete model value
        /// </summary>
        /// <param name="id"></param>
        /// <returns>success</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var book = await this.dbContext.Books.FindAsync(id);
            book.SoftDeleted = true;
            this.dbContext.Books.Update(book);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Change model's information in DB
        /// </summary>
        /// <param name="book"></param>
        /// <returns>success</returns>
        public async Task<bool> UpdateAsync(Book book)
        {
            this.bookValidator.Validate(book).ThrowIfInvalid();
            this.dbContext.Books.Attach(book);
            this.dbContext.Entry(book).State = EntityState.Modified;
            //this.dbContext.Books.Update(book);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Change book's author collection
        /// </summary>
        /// <param name="bookAuthors"></param>
        /// <returns>success</returns>
        public async Task<bool> UpdateAuthorsAsync(BookAuthorsRequest bookAuthors)
        {
            this.bookAuthorsValidator.Validate(bookAuthors).ThrowIfInvalid();

            var oldAuthorsCollection =  await dbContext.Set<BookAuthor>()
                .Where(ba => ba.BookId == bookAuthors.BookId)
                .ToListAsync();

            this.dbContext.BookAuthors.RemoveRange(oldAuthorsCollection);

            var newAuthorsCollection = bookAuthors.AuthorsCollection.ToArray();

            for (int i = 0; i < newAuthorsCollection.Length; i++)
            {
                await dbContext.BookAuthors.AddAsync(
                    new BookAuthor
                    {
                        BookId = bookAuthors.BookId,
                        AuthorId = newAuthorsCollection[i],
                        Order = i                    
                    }
                );
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Sort book's authors
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>sorted collection</returns>
        public IEnumerable<Author> SortAutors(IEnumerable<BookAuthor> collection)
        {
            return collection.OrderBy(ba => ba.Order)
                .Select(ba => ba.Author)
                .Select(a => new Author { Id = a.Id, Name = a.Name})
                .ToList();
        }

        /// <summary>
        /// Change actual price
        /// </summary>
        /// <param name="discountModel"></param>
        /// <returns>success</returns>
        public async Task<bool> UpdateDiscountAsync(DiscountRequest discountModel)
        {
            this.discountValidator.Validate(discountModel).ThrowIfInvalid();

            var booksCollection = await this.dbContext.Books
                .Where(b => discountModel.BooksId.Contains(b.Id))
                .ToListAsync();

            booksCollection.ForEach(b => b.ActualPrice = b.OrgPrice - (b.OrgPrice * discountModel.Discount / 100));
            this.dbContext.Books.UpdateRange(booksCollection);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
