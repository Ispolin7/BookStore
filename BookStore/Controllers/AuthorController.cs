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

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Authors collection</returns>
        [HttpGet]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            var collection = await service.AllAsync();
            var mappedAuthors = this.mapper.Map<AuthorViewModel[]>(collection);
            return Ok(mappedAuthors);
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Author information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAuthorAsync([FromRoute] Guid id)
        {
            var author = await service.GetAsync(id);
            var mappedAuthor = this.mapper.Map<AuthorViewModel>(author);
            return Ok(mappedAuthor);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="author"></param>
        /// <returns>status code</returns>
        [HttpPost]
        public async Task<IActionResult> PostAuthorAsync([FromBody] AuthorRequest author)
        {
            var id = await service.SaveAsync(mapper.Map<Author>(author));
            return CreatedAtAction("GetAuthor", new { id }, null);
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns>status code</returns>
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutAuthorAsync([FromRoute] Guid id, [FromBody] AuthorRequest author)
        {
            author.Id = id;
            await service.UpdateAsync(mapper.Map<Author>(author));
            return NoContent();
        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] Guid id)
        {          
            await this.service.RemoveAsync(id);
            return NoContent();
        }

        // TODO AuthorController test class
    }
}