using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
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

        public BookService(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.books = dbContext.Set<Book>();
        }

        public async Task<IEnumerable<Book>> AllAsync()
        {
            return await this.books.ToListAsync();
        }

        public async Task<Guid> SaveAsync(Book book)
        {
            await this.books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();
            return book.Id;
        }

        public async Task<Book> GetAsync(Guid id)
        {
            return await this.books.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var book = await this.books.FindAsync(id);
            this.books.Remove(book);
            await this.dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateAsync(Book book)
        {
            var oldBook = await this.books
                 .AsNoTracking()
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
    }
}
