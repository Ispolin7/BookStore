using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        public Task<IEnumerable<Book>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
