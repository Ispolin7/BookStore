using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class AuthorUpdateModel : AuthorCreateModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
