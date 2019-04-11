using BookStore.Controllers.ViewModels;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class AuthorService : IAuthorService
    {
        public Task<IEnumerable<AuthorViewModel>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveAsync(AuthorViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AuthorViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
