using BookStore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface ILineItemService
    {       
        Task<Order> CreateRangeAsync(Order order);
        Task<Order> UpdateRangeAsync(Order oldOrder, Order newOrder);
        Task CalculateAllProperties(LineItem item, Guid id, DateTime createTime);
    }
}
