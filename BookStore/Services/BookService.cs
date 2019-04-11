using BookStore.Controllers.ViewModels;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        public Task<IEnumerable<BookViewModel>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveAsync(BookViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(BookViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
