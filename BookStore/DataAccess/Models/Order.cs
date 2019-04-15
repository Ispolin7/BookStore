using System;
using System.Collections.Generic;

namespace BookStore.DataAccess.Models
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

        public string CustomerName { get; set; }
        public IEnumerable<LineItem> LineItems { get; set; }

        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
