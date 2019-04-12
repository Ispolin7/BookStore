using System;
using System.Collections.Generic;

namespace BookStore.DataAccess.Models
{
    public class Book : IEntity
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

        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        public bool SoftDeleted { get; set; }
        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
