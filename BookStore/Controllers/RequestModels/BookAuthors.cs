using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.RequestModels
{
    public class BookAuthors
    {
        public Guid BookId { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<Guid> AuthorsCollection { get; set; }
    }
}
