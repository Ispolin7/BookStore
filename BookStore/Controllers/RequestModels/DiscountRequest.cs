using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class DiscountRequest
    {
        [Required]
        [Range(0, 100)]
        public int Discount { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<Guid> BooksId { get; set; }
    }
}
