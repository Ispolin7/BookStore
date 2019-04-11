using BookStore.Controllers.ViewModels;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class ReviewService : IReviewService
    {
        public Task<IEnumerable<ReviewViewModel>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReviewViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveAsync(ReviewViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ReviewViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
