using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.ValidationModels
{
    public class BookUpdateModel : BookCreateModel
    {
        public Guid Id { get; set; }
    }
}
