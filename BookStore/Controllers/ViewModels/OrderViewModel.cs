using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOrderredUtc { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

        public string CustomerName { get; set; }
        public IEnumerable<LineItemViewModel> LineItem { get; set; }
        //public DateTime CreatedAT { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
