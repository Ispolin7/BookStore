using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.ValidationModels
{
    public class ReviewCreateModel
    {       
        public string VoterName { get; set; }

        public int NumStars { get; set; }

        public string Comment { get; set; }
       
        public Guid BookId { get; set; }
    }
}
