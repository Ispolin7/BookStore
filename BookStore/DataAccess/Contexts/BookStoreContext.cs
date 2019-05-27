using BookStore.DataAccess.Models;
using BookStore.DataAccess.ModelsConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public class BookStoreContext : IdentityDbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
            modelBuilder.ApplyConfiguration(new LineItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

            base.OnModelCreating(modelBuilder);

            // Add in db test values.
            modelBuilder.Seed();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
        {
            foreach (var entry in this.ChangeTracker.Entries<IEntity>())
            {
                var currentDate = DateTime.UtcNow;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAT = currentDate;
                        entry.Entity.UpdatedAt = currentDate;

                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = currentDate;

                        break;
                    case EntityState.Unchanged:
                    case EntityState.Detached:
                    case EntityState.Deleted:
                        break;
                }
            }

            return await (cancellationToken.HasValue
                ? base.SaveChangesAsync(cancellationToken.Value)
                : base.SaveChangesAsync());
        }
    }
}
