using BookStore.Common;
using BookStore.Controllers.ViewModels;
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
        /// Get all models from DB.
        /// </summary>
        /// <returns>models collection</returns>
        public async Task<IEnumerable<Review>> AllAsync()
        {
            return await this.reviews.ToListAsync();
        }

        /// <summary>
        /// Get model's information from DB.
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
        /// <param name="id">entity id</param>
        /// <returns>entity instance with relationships</returns>
        public async Task<Review> GetAsync(Guid id)
        {
            return await this.reviews
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Add new model to DB.
        /// </summary>
        /// <param name="review">entity instance</param>
        /// <returns>model's id</returns>
        public async Task<Guid> SaveAsync(Review review)
        {
            var validation = this.validator.Validate(review);
            validation.ThrowIfInvalid();

            await this.reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return review.Id;
        }

        /// <summary>
        /// Change entitity information in DB.
        /// </summary>
        /// <param name="review">entity instance</param>
        /// <returns>success</returns>
        public async Task<bool> UpdateAsync(Review review)
        {
            this.validator.Validate(review).ThrowIfInvalid();

            var result = this.reviews.Update(review);
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
            var review = await this.reviews.FindAsync(id);
            this.reviews.Remove(review);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public Task<PaginateModel> PaginateAsync(int page, int count)
        {
            throw new NotImplementedException();
        }
    }
}
