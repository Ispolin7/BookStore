using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.RequestModels
{
    public class LineItemCreateModel
    {
        [Required]
        [Range(0, 100)]
        public int NumBooks { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}
