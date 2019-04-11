using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.ViewModels
{
    public class BookViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Publisher { get; set; }

        public double OrgPrice { get; set; }    
        public double ActualPrice { get; set; }

        public string PromotionalText { get; set; }

        public string ImageUrl { get; set; }

        //public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        //public bool SoftDeleted { get; set; }
        //public DateTime CreatedAT { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
