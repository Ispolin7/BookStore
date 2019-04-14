using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BookStoreTests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        //private MockRepository mockRepository;

        //private Mock<BookStoreContext> mockBookStoreContext;
        //private Mock<ILineItemService> mockLineItemService;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    this.mockRepository = new MockRepository(MockBehavior.Strict);

        //    this.mockBookStoreContext = this.mockRepository.Create<BookStoreContext>();
        //    this.mockLineItemService = this.mockRepository.Create<ILineItemService>();
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    this.mockRepository.VerifyAll();
        //}

        //private OrderService CreateService()
        //{
        //    return new OrderService(
        //        this.mockBookStoreContext.Object,
        //        this.mockLineItemService.Object);
        //}

        //[TestMethod]
        //public async Task AllAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();

        //    // Act
        //    var result = await unitUnderTest.AllAsync();

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Guid id = TODO;

        //    // Act
        //    var result = await unitUnderTest.GetAsync(
        //        id);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public async Task SaveAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Order order = TODO;

        //    // Act
        //    var result = await unitUnderTest.SaveAsync(
        //        order);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public async Task RemoveAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Guid id = TODO;

        //    // Act
        //    var result = await unitUnderTest.RemoveAsync(
        //        id);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Order order = TODO;

        //    // Act
        //    var result = await unitUnderTest.UpdateAsync(
        //        order);

        //    // Assert
        //    Assert.Fail();
        //}
    }
}
