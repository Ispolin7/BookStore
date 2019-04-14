using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class LineItemUpdateModel : LineItemCreateModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
