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
    [Route("api/books/{bookId:Guid}/reviews")]
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

        // TODO not found
        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewViewModel>>> GetReviews([FromRoute] Guid bookId)
        {
            var collection = await service.AllBookReviewsAsync(bookId);
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
        public async Task<ActionResult> PostReview([FromRoute] Guid bookId, [FromBody] ReviewCreateModel review)
        {
            review.BookId = bookId;
            var id = await service.SaveAsync(mapper.Map<Review>(review));
            return CreatedAtAction(nameof(GetReview), new { id }, null);
        }

        // PUT: api/reviews/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutReview([FromRoute] Guid id, [FromBody] ReviewUpdateModel review)
        {
            review.Id = id;
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