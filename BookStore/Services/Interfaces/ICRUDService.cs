using BookStore.Controllers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface ICRUDService<T>
    {
        Task<IEnumerable<T>> AllAsync();
        //Task<PaginateModel> PaginateAsync(int page, int count);
        Task<T> GetAsync(Guid id);
        Task<Guid> SaveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(Guid id);
    }
}
