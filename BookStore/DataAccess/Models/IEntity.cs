using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Models
{
    interface IEntity
    {
        Guid Id { get; set; }
        //bool Deleted { get; set; }
        DateTime CreatedAT { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
