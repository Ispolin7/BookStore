using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services.Interfaces;
using AutoMapper;
using BookStore.Controllers.ViewModels;
using BookStore.Controllers.ValidationModels;

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
            return Ok(await service.AllAsync());
        }

        // GET: api/books/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetBook([FromRoute] Guid id)
        {
            return Ok(await service.GetAsync(id));
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult> PostBook([FromBody] BookCreateModel book)
        {           
            var result = await service.SaveAsync(mapper.Map<Book>(book));

            return CreatedAtAction("GetBook", new { id = result });
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
    }
}