using AutoMapper;
using BookStore.Controllers.RequestModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/orders")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.service = orderService ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Order collection</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var collection = await service.AllAsync();
            var mappedOrders = this.mapper.Map<OrderViewModel[]>(collection);
            return Ok(mappedOrders);
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetOrderAsync([FromRoute] Guid id)
        {
            var order = await service.GetAsync(id);
            var mappedOrder = this.mapper.Map<OrderViewModel>(order);
            return Ok(mappedOrder);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>status code</returns>
        [HttpPost]
        public async Task<IActionResult> PostOrderAsync([FromBody] OrderRequest order)
        {
            var result = await service.SaveAsync(mapper.Map<Order>(order));

            return CreatedAtAction(nameof(GetOrderAsync), new { id = result }, null);
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns>status code</returns>
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutOrderAsync([FromRoute] Guid id, [FromBody] OrderRequest order)
        {
            order.Id = id;
            await service.UpdateAsync(mapper.Map<Order>(order));
            return NoContent();
        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] Guid id)
        {
            await this.service.RemoveAsync(id);
            return NoContent();
        }

        // TODO OrderController test class
    }
}