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
    public class ReviewService : IReviewService
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Review> reviews;

        public ReviewService(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.reviews = dbContext.Set<Review>();
        }

        public async Task<IEnumerable<Review>> AllAsync()
        {
            return await this.reviews.ToListAsync();
        }

        public async Task<Review> GetAsync(Guid id)
        {
            return await this.reviews.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Guid> SaveAsync(Review review)
        {
            await this.reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return review.Id;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var review = await this.reviews.FindAsync(id);
            this.reviews.Remove(review);
            await this.dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateAsync(Review review)
        {
            var oldReview = await this.reviews
                 .AsNoTracking()
                 .Where(r => r.Id == review.Id)
                 .FirstOrDefaultAsync();

            oldReview.VoterName = review.VoterName;
            oldReview.NumStars = review.NumStars;
            oldReview.Comment = review.Comment;
            oldReview.UpdatedAt = DateTime.Now;

            var result = this.reviews.Update(oldReview);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
