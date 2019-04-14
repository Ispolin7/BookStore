using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Order> orders;
        private readonly ILineItemService itemService;
        private readonly int deliveryMargin = 3;

        public OrderService(BookStoreContext context, ILineItemService service)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.orders = dbContext.Set<Order>();
            this.itemService = service ?? throw new ArgumentNullException(nameof(itemService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> AllAsync()
        {
            return await this.orders
                .Include(o => o.LineItems)
                    .ThenInclude(i => i.Book)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Order> GetAsync(Guid id)
        {
            return await this.orders
                .Where(a => a.Id == id)
                .Include(o => o.LineItems)
                    .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Guid> SaveAsync(Order order)
        {
            order.CreatedAT = DateTime.UtcNow;
            order.ExpectedDeliveryDate = order.CreatedAT.AddDays(this.deliveryMargin);
            // TODO auto generete id
            order.Id = new Guid();
            await this.orders.AddAsync(await this.itemService.CreateRangeAsync(order));
            await this.dbContext.SaveChangesAsync();
           
            return order.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var order = await this.orders.FindAsync(id);
            this.orders.Remove(order);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Order order)
        {
            var oldOrder = await this.orders
                 .AsNoTracking()
                 .Where(o => o.Id == order.Id)
                 .Include(o => o.LineItems)
                 .FirstOrDefaultAsync();

            oldOrder.CustomerName = order.CustomerName;
            oldOrder.UpdatedAt = DateTime.Now;
                      
            var result = this.orders.Update(await this.itemService.UpdateRangeAsync(oldOrder, order));
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
