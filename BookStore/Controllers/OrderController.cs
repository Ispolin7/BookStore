using AutoMapper;
using BookStore.Controllers.RequestModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrders()
        {
            var collection = await service.AllAsync();
            var mappedOrders = this.mapper.Map<OrderViewModel[]>(collection);
            return Ok(mappedOrders);
        }

        // GET: api/orders/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetOrder([FromRoute] Guid id)
        {
            var order = await service.GetAsync(id);
            var mappedOrder = this.mapper.Map<OrderViewModel>(order);
            return Ok(mappedOrder);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult> PostOrder([FromBody] OrderCreateModel order)
        {
            var result = await service.SaveAsync(mapper.Map<Order>(order));

            return CreatedAtAction(nameof(GetOrder), new { id = result }, null);
        }

        // PUT: api/orders/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutOrder([FromRoute] Guid id, [FromBody] OrderUpdateModel order)
        {
            await service.UpdateAsync(mapper.Map<Order>(order));
            return NoContent();
        }

        // DELETE: api/orders/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            await this.service.RemoveAsync(id);
            return NoContent();
        }
    }
}