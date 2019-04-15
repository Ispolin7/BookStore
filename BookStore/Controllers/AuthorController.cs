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
    [Route("api/authors")]
    [Produces("application/json")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService service;
        private readonly IMapper mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this.service = authorService ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAuthors()
        {
            var collection = await service.AllAsync();
            var mappedAuthors = this.mapper.Map<AuthorViewModel[]>(collection);
            return Ok(mappedAuthors);
        }

        // GET: api/Author/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetAuthor([FromRoute] Guid id)
        {
            var author = await service.GetAsync(id);
            var mappedAuthor = this.mapper.Map<AuthorViewModel>(author);
            return Ok(mappedAuthor);
        }

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult> PostAuthor([FromBody] AuthorCreateModel author)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var id = await service.SaveAsync(mapper.Map<Author>(author));

            return CreatedAtAction("GetAuthor", new { id }, null);
        }

        // PUT: api/Author/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutAuthor([FromRoute] Guid id, [FromBody] AuthorUpdateModel author)
        {           
            await service.UpdateAsync(mapper.Map<Author>(author));
            return NoContent();
        }


        // DELETE: api/Author/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] Guid id)
        {          
            await this.service.RemoveAsync(id);
            return NoContent();
        }

        // TODO AuthorController test class
    }
}