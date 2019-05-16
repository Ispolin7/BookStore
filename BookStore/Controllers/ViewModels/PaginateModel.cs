using BookStore.DataAccess.Models;
using BookStore.Services.Models;
using System.Collections.Generic;

namespace BookStore.Controllers.ViewModels
{
    public class PaginateModel
    {
        public IEnumerable<IEntity> Entities { get; set; }
        public PageInfo PageInfo { get; set; }

        public PaginateModel(IEnumerable<IEntity> entities, PageInfo pageInfo)
        {
            this.Entities = entities;
            this.PageInfo = pageInfo;
        }
    }
}
