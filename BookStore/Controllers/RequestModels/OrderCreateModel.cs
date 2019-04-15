using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class OrderCreateModel
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string CustomerName { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<LineItemCreateModel> LineItems { get; set; }
    }
}
