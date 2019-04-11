using BookStore.Controllers.ViewModels;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {
        public Task<IEnumerable<OrderViewModel>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveAsync(OrderViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(OrderViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
