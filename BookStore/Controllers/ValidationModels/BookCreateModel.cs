using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.ValidationModels
{
    public class BookCreateModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string PublishedOn { get; set; }

        public string Publisher { get; set; }

        public double OrgPrice { get; set; }
        public double ActualPrice { get; set; }

        public string PromotionalText { get; set; }

        public string ImageUrl { get; set; }
    }
}
