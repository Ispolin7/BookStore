using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class AuthorCreateModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
