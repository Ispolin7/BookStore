using System;

namespace BookStore.Controllers.ViewModels
{
    public class LineItemViewModel
    {
        public Guid Id { get; set; }

        public string LineNum { get; set; }

        public int NumBooks { get; set; }

        public double BookPrice { get; set; }

        public BookViewModel Book { get; set; }
        public Guid BookId { get; set; }

        //public OrderViewModel Order { get; set; }
        public Guid OrderId { get; set; }

        //public DateTime CreatedAT { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
