using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Models
{
    public class LineItem : IEntity
    {
        public Guid Id { get; set; }

        public string LineNum { get; set; }

        public int NumBooks { get; set; }

        public double BookPrice { get; set; }

        public Book Book { get; set; }
        public Guid BookId { get; set; }

        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
