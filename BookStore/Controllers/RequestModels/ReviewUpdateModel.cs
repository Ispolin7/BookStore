using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class ReviewUpdateModel : ReviewCreateModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
