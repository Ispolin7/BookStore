using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class LineItemRequest
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0, 100)]
        public int NumBooks { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}
