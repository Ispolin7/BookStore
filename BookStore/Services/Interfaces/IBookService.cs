using BookStore.Controllers.RequestModels;
using BookStore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IBookService : ICRUDService<Book>
    {
        Task<bool> UpdateAuthorsAsync(BookAuthors bookAuthors);
        Task<bool> UpdateDiscountAsync(DiscountModel discountModel);
    }
}
