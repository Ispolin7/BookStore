using BookStore.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStore.DataAccess
{
    public static class ModelBuilderSeed
    {       
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(CollectionsFactory.GetAuthorsCollection().ToArray());
            modelBuilder.Entity<Book>().HasData(CollectionsFactory.GetBooksCollection().ToArray());
            modelBuilder.Entity<BookAuthor>().HasData(CollectionsFactory.GetBookAuthorsCollection().ToArray());
            modelBuilder.Entity<LineItem>().HasData(CollectionsFactory.GetLineItemsCollection().ToArray());
            modelBuilder.Entity<Order>().HasData(CollectionsFactory.GetOrdersCollection().ToArray());
            modelBuilder.Entity<Review>().HasData(CollectionsFactory.GetReviewsCollection().ToArray());

            modelBuilder.Entity<IdentityUser>().HasData(CollectionsFactory.GetUsers().ToArray());
            modelBuilder.Entity<IdentityRole>().HasData(CollectionsFactory.GetRoles().ToArray());
            modelBuilder.Entity <IdentityUserRole<string>>().HasData(CollectionsFactory.GetUserRole().ToArray());
        }
    }
}
