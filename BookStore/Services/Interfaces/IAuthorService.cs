using BookStore.Controllers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    interface IAuthorService : ICRUDService<AuthorViewModel>
    {
    }
}
