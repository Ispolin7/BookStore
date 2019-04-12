using AutoMapper;
using BookStore.Controllers.ValidationModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/reviews")]
    [Produces("application/json")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService service;
        private readonly IMapper mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            this.service = reviewService ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewViewModel>>> GetReviews()
        {
            var collection = await service.AllAsync();
            var mappedReview = this.mapper.Map<ReviewViewModel[]>(collection);
            return Ok(mappedReview);
        }

        // GET: api/reviews/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetReview([FromRoute] Guid id)
        {
            var review = await service.GetAsync(id);
            var mappedReview = this.mapper.Map<ReviewViewModel>(review);
            return Ok(mappedReview);
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult> PostReview([FromBody] ReviewCreateModel review)
        {
            var id = await service.SaveAsync(mapper.Map<Review>(review));
            return CreatedAtAction(nameof(GetReview), new { id }, null);
        }

        // PUT: api/reviews/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutReview([FromRoute] Guid id, [FromBody] ReviewUpdateModel review)
        {
            await service.UpdateAsync(mapper.Map<Review>(review));
            return NoContent();
        }

        // DELETE: api/reviews/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
        {
            await this.service.RemoveAsync(id);
            return NoContent();
        }
    }
}