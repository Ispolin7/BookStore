﻿using BookStore.Common;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly BookStoreContext dbContext;        
        private readonly ILineItemService itemService;
        private readonly IValidator<Order> orderValidator;
        private readonly int deliveryMargin = 3;

        public OrderService(BookStoreContext context, ILineItemService service, IValidator<Order> validator)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
            this.itemService = service ?? throw new ArgumentNullException(nameof(itemService));
            this.orderValidator = validator ?? throw new ArgumentNullException(nameof(orderValidator));
        }

        /// <summary>
        /// Get all models from DB.
        /// </summary>
        /// <returns>models collection</returns>
        public async Task<IEnumerable<Order>> AllAsync()
        {
            return await this.dbContext.Orders
                .Include(o => o.LineItems)
                    .ThenInclude(i => i.Book)
                .ToListAsync();
        }

        /// <summary>
        /// Get model's information from DB.
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>entity instance with relationships</returns>
        public async Task<Order> GetAsync(Guid id)
        {
            return await this.dbContext.Orders
                .Where(a => a.Id == id)
                .Include(o => o.LineItems)
                    .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Add new model to DB.
        /// </summary>
        /// <param name="order">entity instance</param>
        /// <returns>model's id</returns>
        public async Task<Guid> SaveAsync(Order order)
        {
            this.orderValidator.Validate(order).ThrowIfInvalid();

            order.CreatedAT = DateTime.UtcNow;
            order.ExpectedDeliveryDate = order.CreatedAT.AddDays(this.deliveryMargin);
            order.Id = new Guid();

            await this.dbContext.Orders.AddAsync(await this.itemService.CreateRangeAsync(order));
            await this.dbContext.SaveChangesAsync();
           
            return order.Id;
        }

        /// <summary>
        /// Delete entity in DB.
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>success</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var order = await this.dbContext.Orders.FindAsync(id);
            this.dbContext.Orders.Remove(order);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Change entitity information in DB.
        /// </summary>
        /// <param name="order">entity instance</param>
        /// <returns>success</returns>
        public async Task<bool> UpdateAsync(Order order)
        {
            this.orderValidator.Validate(order).ThrowIfInvalid();

            var oldOrder = await this.dbContext.Orders
                 .Where(o => o.Id == order.Id)
                 .Include(o => o.LineItems)
                 .FirstOrDefaultAsync();

            var updatedOrder = await this.itemService.UpdateRangeAsync(oldOrder, order);
            var result = this.dbContext.Orders.Update(updatedOrder);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public Task<PaginateModel> PaginateAsync(int page, int count)
        {
            throw new NotImplementedException();
        }
    }
}
