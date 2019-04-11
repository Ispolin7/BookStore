using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Models
{
    public class Author : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<BookAuthor> BookAuthors { get; set; }

        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
