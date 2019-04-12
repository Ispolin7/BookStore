using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class ReviewService : IReviewService
    {
        public Task<IEnumerable<Review>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
