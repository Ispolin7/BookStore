using BookStore.Common;
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
    public class ReviewService : IReviewService
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Review> reviews;
        private readonly IValidator<Review> validator;

        public ReviewService(BookStoreContext context, IValidator<Review> reviewValidator)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.reviews = dbContext.Set<Review>();
            this.validator = reviewValidator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Review>> AllAsync()
        {
            return await this.reviews.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Review>> AllBookReviewsAsync(Guid bookId)
        {
            return await this.reviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Review> GetAsync(Guid id)
        {
            return await this.reviews
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public async Task<Guid> SaveAsync(Review review)
        {
            var validation = this.validator.Validate(review);
            validation.ThrowIfInvalid();

            await this.reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return review.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Review review)
        {
            var validation = this.validator.Validate(review);
            validation.ThrowIfInvalid();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var review = await this.reviews.FindAsync(id);
            this.reviews.Remove(review);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
