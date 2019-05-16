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

        /// <summary>
        /// Get all models from DB.
        /// </summary>
        /// <returns>models collection</returns>
        public async Task<IEnumerable<Author>> AllAsync()
        {
            return await this.authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .ToListAsync();
        }

        /// <summary>
        /// Get model's information from DB.
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>entity instance with relationships</returns>
        public async Task<Author> GetAsync(Guid id)
        {
            return await this.authors
                .Where(a => a.Id == id)
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Add new model to DB.
        /// </summary>
        /// <param name="author">entity instance</param>
        /// <returns>model's id</returns>
        public async Task<Guid> SaveAsync(Author author)
        {
            await this.authors.AddAsync(author);
            await this.dbContext.SaveChangesAsync();
            return author.Id;
        }

        /// <summary>
        /// Change entitity information in DB.
        /// </summary>
        /// <param name="author">entity instance</param>
        /// <returns>success</returns>
        public async Task<bool> UpdateAsync(Author author)
        {
            var result = this.authors.Update(author);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete entity in DB.
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>success</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var author = await this.authors.FindAsync(id);
            this.authors.Remove(author);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public Task<PaginateModel> PaginateAsync(int page, int count)
        {
            throw new NotImplementedException();
        }
    }
}
