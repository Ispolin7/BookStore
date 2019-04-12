using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;

namespace BookStore.Services.Interfaces
{
    public interface IOrderService : ICRUDService<Order>
    {
    }
}
