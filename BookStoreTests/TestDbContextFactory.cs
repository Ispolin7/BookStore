using BookStore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiYardTests
{
    class TestDbContextFactory
    {
        private readonly BookStoreContext context;

        public TestDbContextFactory()
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
        /// <returns></returns>
        public BookStoreContext GetContext()
        {
            return this.context;
        }
    }
}
