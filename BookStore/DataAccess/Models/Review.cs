using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Models
{
    public class Review : IEntity
    {
        public Guid Id { get; set; }

        public string VoterName { get; set; }

        public int NumStars { get; set; }

        public string Comment { get; set; }

        public Book Book { get; set; }
        public Guid BookId { get; set; }

        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
