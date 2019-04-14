using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class OrderUpdateModel : OrderCreateModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
