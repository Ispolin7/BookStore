using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class LineItemService : ILineItemService
    {
        private readonly BookStoreContext dbContext;

        public LineItemService(BookStoreContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> CreateRangeAsync(Order order)
        {
            foreach (var item in order.LineItems)
            {
                await this.CalculateAllProperties(item, order.Id, order.CreatedAT);
            }
            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldOrder"></param>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public async Task<Order> UpdateRangeAsync(Order oldOrder, Order newOrder)
        {
            foreach (var item in newOrder.LineItems)
            {
                await this.CalculateAllProperties(item, newOrder.Id, newOrder.CreatedAT);
            }

            dbContext.Set<LineItem>().RemoveRange(oldOrder.LineItems);
            oldOrder.LineItems = newOrder.LineItems;         

            return oldOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        public async Task CalculateAllProperties(LineItem item, Guid id, DateTime createTime)
        {
            item.OrderId = id;
            var book = await dbContext.Set<Book>().Where(b => b.Id == item.BookId).FirstOrDefaultAsync();
            item.LineNum = new Random().Next(0000000, 9999999).ToString();
            item.BookPrice = item.NumBooks * book.ActualPrice;
            //item.CreatedAT = createTime;
        }
    }
}
