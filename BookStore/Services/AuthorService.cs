using BookStore.Controllers.ViewModels;
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
    public class AuthorService : IAuthorService
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Author> authors;

        public AuthorService(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.authors = dbContext.Set<Author>();
        }

        public async Task<IEnumerable<Author>> AllAsync()
        {
            return await this.authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .ToListAsync();
        }

        public async Task<Author> GetAsync(Guid id)
        {
            return await this.authors
                .Where(a => a.Id == id)
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> SaveAsync(Author author)
        {
            await this.authors.AddAsync(author);
            await this.dbContext.SaveChangesAsync();
            return author.Id;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var author = await this.authors.FindAsync(id);
            this.authors.Remove(author);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var oldAuthor = await this.authors
                //.AsNoTracking()
                .Where(a => a.Id == author.Id)
                .FirstOrDefaultAsync();

            oldAuthor.Name = author.Name;
            oldAuthor.UpdatedAt = DateTime.UtcNow;

            var result = this.authors.Update(oldAuthor);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
