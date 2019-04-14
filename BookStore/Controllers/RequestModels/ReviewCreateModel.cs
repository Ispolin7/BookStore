using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class ReviewCreateModel
    {     
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string VoterName { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumStars { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Comment { get; set; }
       
        [Required]
        public Guid BookId { get; set; }
    }
}
