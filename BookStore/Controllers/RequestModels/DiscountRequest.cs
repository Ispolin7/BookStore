using System;
using System.Collections.Generic;

namespace BookStore.Controllers.RequestModels
{
    public class DiscountRequest
    {
        public int Discount { get; set; }
        public IEnumerable<Guid> BooksId { get; set; }
    }
}
