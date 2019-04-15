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
        private readonly DbContext dbContext;
        private readonly DbSet<Book> books;
        private readonly IValidator<Book> bookValidator;

        public BookService(BookStoreContext context, IValidator<Book> validator)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.books = dbContext.Set<Book>();
            this.bookValidator = validator ?? throw new ArgumentNullException(nameof(bookValidator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> AllAsync()
        {
            var collection = await this.books
                .AsNoTracking()
                .Where(b => b.SoftDeleted == false)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();

            collection.ForEach(b => b.Authors = this.SortAutors(b.BookAuthors));
            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetAsync(Guid id)
        {
            var book = await this.books
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
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<Guid> SaveAsync(Book book)
        {
            this.bookValidator.Validate(book).ThrowIfInvalid();
            await this.books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();
            return book.Id;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var book = await this.books.FindAsync(id);
            book.SoftDeleted = true;
            this.books.Update(book);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Book book)
        {
            this.bookValidator.Validate(book).ThrowIfInvalid();

            var oldBook = await this.books
                 //.AsNoTracking()
                 .Where(a => a.Id == book.Id)
                 .FirstOrDefaultAsync();

            oldBook.Title = book.Title;
            oldBook.Description = book.Description;
            oldBook.PublishedOn = book.PublishedOn;
            oldBook.Publisher = book.Publisher;
            oldBook.OrgPrice = book.OrgPrice;
            oldBook.ActualPrice = book.ActualPrice;
            oldBook.PromotionalText = book.PromotionalText;
            oldBook.ImageUrl = book.ImageUrl;
            oldBook.UpdatedAt = DateTime.Now;

            var result = this.books.Update(oldBook);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookAuthors"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAuthorsAsync(BookAuthors bookAuthors)
        {
            var oldAuthorsCollection =  await dbContext.Set<BookAuthor>()
                .Where(ba => ba.BookId == bookAuthors.BookId)
                .ToListAsync();

            dbContext.Set<BookAuthor>().RemoveRange(oldAuthorsCollection);

            var newAuthorsCollection = bookAuthors.AuthorsCollection.ToArray();

            for (int i = 0; i < newAuthorsCollection.Length; i++)
            {
                await dbContext.Set<BookAuthor>().AddAsync(
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
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IEnumerable<Author> SortAutors(IEnumerable<BookAuthor> collection)
        {
            return collection.OrderBy(ba => ba.Order)
                .Select(ba => ba.Author)
                .Select(a => new Author { Id = a.Id, Name = a.Name})
                .ToList();
        }
    }
}
