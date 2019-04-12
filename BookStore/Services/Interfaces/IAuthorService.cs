using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IAuthorService : ICRUDService<Author>
    {
    }
}
