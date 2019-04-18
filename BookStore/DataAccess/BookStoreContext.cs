using BookStore.DataAccess.Models;
using BookStore.DataAccess.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public class BookStoreContext : DbContext
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
                //var userId = this._currentUserService.CurrentUserId;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAT = currentDate;
                        entry.Entity.UpdatedAt = currentDate;

                        break;
                    case EntityState.Modified:
                        //entry.Entity.CreatedAT.
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


        //public async Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
        //{
        //    foreach (var entry in this.ChangeTracker.Entries<IAuditEntity>())
        //    {
        //        var currentDate = this._clock.UtcNow;
        //        var userId = this._currentUserService.CurrentUserId;

        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = userId;
        //                entry.Entity.CreatedOn = currentDate;

        //                entry.Entity.ModifiedBy = userId;
        //                entry.Entity.ModifiedOn = currentDate;

        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.ModifiedBy = userId;
        //                entry.Entity.ModifiedOn = currentDate;

        //                break;
        //            case EntityState.Unchanged:
        //            case EntityState.Detached:
        //            case EntityState.Deleted:
        //                break;
        //        }
        //    }

        //    return await (cancellationToken.HasValue
        //        ? base.SaveChangesAsync(cancellationToken.Value)
        //        : base.SaveChangesAsync());
        //}

        //public void Delete<T>(T entity, bool permanently = false) where T : class, IEntity
        //{
        //    if (!typeof(IAuditEntity).IsAssignableFrom(typeof(T)) || permanently)
        //    {
        //        this.Set<T>().Remove(entity);
        //        return;
        //    }

        //    ((IAuditEntity)entity).DeletedOn = this._clock.UtcNow;
        //    this.Update(entity);
        //}
    }
}
