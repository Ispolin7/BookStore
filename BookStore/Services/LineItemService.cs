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
                await this.CalculateAllProperties(item);
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
                await this.CalculateAllProperties(item);
            }

            dbContext.LineItems.RemoveRange(oldOrder.LineItems);       

            return newOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task CalculateAllProperties(LineItem item)
        {
            //item.OrderId = id;
            var book = await dbContext.Books.Where(b => b.Id == item.BookId).FirstOrDefaultAsync();
            item.LineNum = new Random().Next(0000000, 9999999).ToString();
            item.BookPrice = item.NumBooks * book.ActualPrice;
        }
    }
}
