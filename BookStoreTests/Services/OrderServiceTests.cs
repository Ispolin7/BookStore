using BookStore.Common;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using BookStore.Services.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYardTests;

namespace BookStoreTests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private BookStoreContext dbContext;
        private Mock<ILineItemService> mockLineItemService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockLineItemService = new Mock<ILineItemService>();           
            var contextBuilder = new TestDbContextBuilder();
            contextBuilder.FillDb(CollectionsFactory.GetOrdersCollection());
            contextBuilder.FillDb(CollectionsFactory.GetLineItemsCollection());
            contextBuilder.FillDb(CollectionsFactory.GetBooksCollection());
            this.dbContext = contextBuilder.BuildContext();
        }

        [TestMethod]
        public async Task AllAsync_GetAllOrders_ExpectedCorrectAmount()
        {
            // Arrange
            var service = this.CreateService();
            var expectedCount = this.CountOrdersInCollection();

            // Act
            var result = await service.AllAsync();
            var realCount = result.Count();

            // Assert
            Assert.AreEqual(realCount, expectedCount, $"Expected - {expectedCount}, real - {realCount}");
        }

        [TestMethod]
        public async Task GetAsync_GetOrderInformation_ExpectedCorrectDependensiecAmount()
        {
            // Arrange
            var service = this.CreateService();
            var testOrder = this.GetTestOrder();
            var expectedLineItems = CollectionsFactory.GetLineItemsCollection()
                .Where(i => i.OrderId == testOrder.Id)
                .Count();

            // Act
            var result = await service.GetAsync(testOrder.Id);
            var realLineItems = result.LineItems.Count();

            // Assert
            Assert.AreEqual(expectedLineItems, realLineItems, $"Expected - {expectedLineItems} LineItems, real - {realLineItems}");
        }

        [TestMethod]
        public async Task SaveAsync_AddNewOrderToDb_ExpectedIncrementAmount()
        {
            // Arrange
            var order = GetOrder();
            var mockedOrder = GetMockedOrder();

            mockLineItemService.Setup(s => s.CreateRangeAsync(It.IsAny<Order>())).Returns(Task.FromResult(mockedOrder));
            var service = this.CreateService();
            var beforeOrders = await service.AllAsync();
            var expectedOrderCount = beforeOrders.Count() + 1;

            // Act
            var result = await service.SaveAsync(order);
            var afterOrders = await service.AllAsync();
            var afterOrderCount = afterOrders.Count();

            // Assert
            Assert.AreEqual(expectedOrderCount, afterOrderCount, $"Expected - {expectedOrderCount} LineItems, real - {afterOrderCount}");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task SaveAsync_AddNewOrderToDb_ExpectedValidationException()
        {
            // Arrange     
            var service = this.CreateService();
            var testOrder = this.GetTestOrder();
            testOrder.LineItems = new List<LineItem>
            {
                new LineItem
                {
                    BookId = new Guid()
                }
            };

            // Act
            await service.SaveAsync(testOrder);
        }

        [TestMethod]
        public async Task RemoveAsync_DeleteOrderInDb_ExpectedDecrementCount()
        {
            // Arrange
            var service = this.CreateService();
            var expectedCount = CountOrdersInCollection() - 1;
            var testOrder = GetTestOrder();

            // Act
            var result = await service.RemoveAsync(testOrder.Id);
            var newOrderCollection = await service.AllAsync();
            var newCount = newOrderCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        // TODO System.InvalidOperationException: The instance of entity type 'Order' cannot be tracked because another instance with the key value '{Id: cc32fdc5-1847-423d-85a3-3b7a8b6d19b0}' is already being tracked. 
        //[TestMethod]
        public async Task UpdateAsync_UpdateOrderInformation_ExpectedSuccess()
        {
            // Arrange
            var order = GetOrder();
            order.Id = CollectionsFactory.GetOrdersCollection().First().Id;

            var mockedOrder = GetMockedOrder();
            mockedOrder.Id = order.Id;
            mockLineItemService
                .Setup(s => s.UpdateRangeAsync(It.IsAny<Order>(), It.IsAny<Order>()))
                .Returns(Task.FromResult(mockedOrder));

            var service = CreateService();

            // Act
            var result = await service.UpdateAsync(order);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Create moq order instance
        /// </summary>
        /// <returns>order with lone items collection</returns>
        public Order GetMockedOrder()
        {
            return new Order
            {
                CustomerName = "Test Customer",
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        NumBooks = 2,
                        BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                        LineNum = "123456",
                        BookPrice = 100
                    },
                    new LineItem
                    {
                        NumBooks = 1,
                        BookId = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf"),
                        LineNum = "1234567",
                        BookPrice = 200
                    }
                },
                CreatedAT = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Create new order instance
        /// </summary>
        /// <returns>order with lone items collection</returns>
        public Order GetOrder()
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

        /// <summary>
        /// Get test order from test collection
        /// </summary>
        /// <returns>order instance</returns>
        public Order GetTestOrder()
        {
            return CollectionsFactory.GetOrdersCollection().First();
        }

        /// <summary>
        /// Count the number of orders in test collection.
        /// </summary>
        /// <returns></returns>
        public int CountOrdersInCollection()
        {
            return CollectionsFactory.GetOrdersCollection().Count();
        }

        /// <summary>
        /// Create new OrderService instance
        /// </summary>
        /// <returns>order service</returns>
        private OrderService CreateService()
        {
            return new OrderService(
                this.dbContext,
                this.mockLineItemService.Object,
                new OrderValidator(this.dbContext));
        }
    }
}
