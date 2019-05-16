using AutoMapper;
using BookStore.Controllers.Filters;
using BookStore.Controllers.RequestModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/books")]
    [Produces("application/json")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;
        private readonly IMapper mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            this.service = bookService ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Books collection</returns>
        [HttpGet]
        //[Pagination]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetbooksAsync()
        {
            var collection = await service.AllAsync();
            var mappedBooks = this.mapper.Map<BookViewModel[]>(collection);
            return Ok(mappedBooks);
        }

        ///// <summary>
        ///// Display a listing of the resource.
        ///// </summary>
        ///// <returns>Books collection</returns>
        //[HttpGet("paginate")]
        //[Pagination]
        //public async Task<IActionResult> GetCurrentPageCollectionAsync([FromQuery] int page, int count)
        //{
        //    var pageCollection = await service.PaginateAsync(page, count);
        //    return Ok(new
        //    {
        //        Books = this.mapper.Map<BookViewModel[]>(pageCollection.Entities),
        //        PageInfo = pageCollection.PageInfo
        //    });
        //}

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Book information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBookAsync([FromRoute] Guid id)
        {
            var book = await service.GetAsync(id);
            var mappedBook = this.mapper.Map<BookViewModel>(book);
            return Ok(mappedBook);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>status code</returns>
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> PostBookAsync([FromBody] BookRequest book)
        {           
            var id = await service.SaveAsync(mapper.Map<Book>(book));
            return CreatedAtAction(nameof(GetBookAsync), new { id }, null);
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns>status code</returns>
        [HttpPut("{Id:Guid}")]
        [ValidateModelState]
        public async Task<IActionResult> PutBookAsync([FromRoute] Guid Id, [FromBody] BookRequest book)
        {
            //book.Id = id;
            await service.UpdateAsync(mapper.Map<Book>(book));
            return NoContent();
        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] Guid id)
        {            
            await this.service.RemoveAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Update book's authors
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookAuthors"></param>
        /// <returns>status code</returns>
        [HttpPut("{id:Guid}/authors")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateAuthorsAsync([FromRoute] Guid id, [FromBody] BookAuthorsRequest bookAuthors)
        {
            bookAuthors.BookId = id;
            await service.UpdateAuthorsAsync(bookAuthors);
            return NoContent();
        }

        /// <summary>
        /// Add/change book's discount
        /// </summary>
        /// <param name="discountModel"></param>
        /// <returns>status code</returns>
        [HttpPut("discount")]
        [ValidateModelState]
        public async Task<IActionResult> ChangeDiscountAsync([FromBody] DiscountRequest discountModel)
        {
            await service.UpdateDiscountAsync(discountModel);
            return NoContent();
        }
    }
}