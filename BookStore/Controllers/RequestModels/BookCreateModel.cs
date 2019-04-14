using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class BookCreateModel
    {
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(3000, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        public string PublishedOn { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Publisher { get; set; }

        [Range(0.1, Double.MaxValue)]
        public double OrgPrice { get; set; }

        [Range(0.1, Double.MaxValue)]
        public double ActualPrice { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        public string PromotionalText { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
