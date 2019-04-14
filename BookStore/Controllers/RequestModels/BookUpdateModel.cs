using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class BookUpdateModel : BookCreateModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
