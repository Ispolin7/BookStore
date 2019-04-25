using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYardTests;

namespace BookStoreTests.Services
{
    [TestClass]
    public class LineItemServiceTests
    {

        private ILineItemService service;
        private BookStoreContext dbContext;
        
        [TestInitialize]
        public void TestInitialize()
        {
            var contextBuilder = new TestDbContextBuilder();
            contextBuilder.FillDb(CollectionsFactory.GetBooksCollection());
            contextBuilder.FillDb(CollectionsFactory.GetLineItemsCollection());
            contextBuilder.FillDb(CollectionsFactory.GetOrdersCollection());
            this.dbContext = contextBuilder.BuildContext();
            this.service = new LineItemService(this.dbContext);
        }

        [TestMethod]
        public async Task CreateRangeAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testOrder = this.GetTestOrder();

            // Act
            await this.service.CreateRangeAsync(testOrder);

            var LineNumsCollection = testOrder.LineItems.Select(li => li.LineNum).ToList();
            var BookPricesCollection = testOrder.LineItems.Select(li => li.BookPrice).ToList();

            // Assert
            Assert.IsFalse(LineNumsCollection.Contains(null), "LineNums collection contains null value");
            Assert.IsFalse(BookPricesCollection.Contains(0), "BookPrice collection contains 0 value");
        }

        //[TestMethod]
        public async Task UpdateRangeAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testLineItem = CollectionsFactory.GetLineItemsCollection().First();
            var oldOrder = dbContext.Orders.Where(o => o.Id == testLineItem.OrderId).Include(o => o.LineItems).First();
            var newOrder = this.GetTestOrder();
            newOrder.Id = oldOrder.Id;

            // Act
            await this.service.UpdateRangeAsync(oldOrder, newOrder);
            var LineNumsCollection = newOrder.LineItems.Select(li => li.LineNum).ToList();
            var BookPricesCollection = newOrder.LineItems.Select(li => li.BookPrice).ToList();
            dbContext.SaveChanges();
            var IsExistInDb = this.dbContext.LineItems.Where(li => li.Id == testLineItem.Id).Any();

            // Assert
            Assert.IsFalse(LineNumsCollection.Contains(null), "LineNums collection contains null value");
            Assert.IsFalse(BookPricesCollection.Contains(0), "BookPrice collection contains 0 value");
            Assert.IsFalse(IsExistInDb, "Old line item not deleted");
        }

        [TestMethod]
        public async Task CalculateAllProperties_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testOrder = this.GetTestOrder();
            var testLineItem = testOrder.LineItems.First();

            // Act
            await this.service.CalculateAllProperties(testLineItem);

            // Assert
            Assert.IsTrue(testLineItem.LineNum != null, "LineNum is not calculated");
            Assert.IsTrue(testLineItem.BookPrice != 0 , "BookPrice is not calculated");
        }

        /// <summary>
        /// Create new order instance
        /// </summary>
        /// <returns>order with lone items collection</returns>
        public Order GetTestOrder()
        {
            return new Order
            {
                CustomerName = "Test Customer",
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        NumBooks = 2,
                        BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f")
                    },
                    new LineItem
                    {
                        NumBooks = 1,
                        BookId = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf")
                    }
                }
            };
        }
    }
}
