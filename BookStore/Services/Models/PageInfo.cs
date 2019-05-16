using System;

namespace BookStore.Services.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } 
        //public int PageSize { get; set; } 
        public int TotalItems { get; set; } 
        public int TotalPages { get; set; }
        //{
        //    get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        //}

        public PageInfo(int page, int size, int items)
        {
            this.PageNumber = page;
            //this.PageSize = size;
            this.TotalItems = items;
            this.TotalPages = (int)Math.Ceiling((decimal)items / size);
        }
    }
}
