using BookStore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiYardTests
{
    public class TestDbContextBuilder
    {
        private readonly BookStoreContext context;

        public TestDbContextBuilder()
        {
            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseInMemoryDatabase(databaseName: "TestBookSroreDataBase")
                .EnableSensitiveDataLogging()
                .Options;

            this.context = new BookStoreContext(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool FillDb<T>(IEnumerable<T> collection) where T : class
        {
            context.Set<T>().RemoveRange(context.Set<T>());
            context.Set<T>().AddRange(collection);
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BookStoreContext BuildContext()
        {
            return this.context;
        }
    }
}
