using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class BookAuthorsRequest
    {
        public Guid BookId { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<Guid> AuthorsCollection { get; set; }
    }
}
