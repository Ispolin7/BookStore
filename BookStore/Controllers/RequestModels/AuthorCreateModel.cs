using System.ComponentModel.DataAnnotations;

namespace BookStore.Controllers.RequestModels
{
    public class AuthorCreateModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
