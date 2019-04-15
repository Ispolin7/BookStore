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

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> Getbooks()
        {
            var collection = await service.AllAsync();
            var mappedBooks = this.mapper.Map<BookViewModel[]>(collection);
            return Ok(mappedBooks);
        }

        // GET: api/books/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetBook([FromRoute] Guid id)
        {
            var book = await service.GetAsync(id);
            var mappedBook = this.mapper.Map<BookViewModel>(book);
            return Ok(mappedBook);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult> PostBook([FromBody] BookCreateModel book)
        {           
            var id = await service.SaveAsync(mapper.Map<Book>(book));

            return CreatedAtAction(nameof(GetBook), new { id }, null);
        }

        // PUT: api/books/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutBook([FromRoute] Guid id, [FromBody] BookUpdateModel book)
        {            
            await service.UpdateAsync(mapper.Map<Book>(book));
            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {            
            await this.service.RemoveAsync(id);
            return NoContent();
        }

        
        [HttpPut("{id:Guid}/authors")]
        public async Task<IActionResult> UpdateAuthors([FromRoute] Guid id, [FromBody] BookAuthors bookAuthors)
        {
            bookAuthors.BookId = id;
            await service.UpdateAuthorsAsync(bookAuthors);
            return NoContent();
        }

        // TODO add route for change actualPrice
        // TODO BookController test class
    }
}