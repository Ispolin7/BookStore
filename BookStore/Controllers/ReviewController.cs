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

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>Review collection</returns>
        [HttpGet]
        public async Task<IActionResult> GetReviewsAsync([FromRoute] Guid bookId)
        {
            var collection = await service.AllBookReviewsAsync(bookId);
            var mappedReview = this.mapper.Map<ReviewViewModel[]>(collection);
            return Ok(mappedReview);
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Review information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetReviewAsync([FromRoute] Guid id)
        {
            var review = await service.GetAsync(id);
            var mappedReview = this.mapper.Map<ReviewViewModel>(review);
            return Ok(mappedReview);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="review"></param>
        /// <returns>status code</returns>
        [HttpPost]
        public async Task<IActionResult> PostReviewAsync([FromRoute] Guid bookId, [FromBody] ReviewRequest review)
        {
            review.BookId = bookId;
            var id = await service.SaveAsync(mapper.Map<Review>(review));
            return CreatedAtAction(nameof(GetReviewAsync), new { id }, null);
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="review"></param>
        /// <returns>status code</returns>
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutReviewAsync([FromRoute] Guid id, [FromBody] ReviewRequest review)
        {
            review.Id = id;
            await service.UpdateAsync(mapper.Map<Review>(review));
            return NoContent();
        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] Guid id)
        {
            await this.service.RemoveAsync(id);
            return NoContent();
        }
        // TODO ReviewController test class
    }
}