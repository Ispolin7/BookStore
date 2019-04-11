using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.ViewModels
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }

        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
