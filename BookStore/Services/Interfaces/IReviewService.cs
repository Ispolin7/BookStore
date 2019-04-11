using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Controllers.ViewModels;

namespace BookStore.Services.Interfaces
{
    interface IReviewService : ICRUDService<ReviewViewModel>
    {
    }
}
