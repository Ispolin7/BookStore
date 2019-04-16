using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.RequestModels
{
    public class DiscountModel
    {
        public int Discount { get; set; }
        public IEnumerable<Guid> BooksId { get; set; }
    }
}
