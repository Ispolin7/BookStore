using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BookStoreTests.Services
{
    [TestClass]
    public class LineItemServiceTests
    {
        //private MockRepository mockRepository;

        //private Mock<BookStoreContext> mockBookStoreContext;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    this.mockRepository = new MockRepository(MockBehavior.Strict);

        //    this.mockBookStoreContext = this.mockRepository.Create<BookStoreContext>();
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    this.mockRepository.VerifyAll();
        //}

        //private LineItemService CreateService()
        //{
        //    return new LineItemService(
        //        this.mockBookStoreContext.Object);
        //}

        //[TestMethod]
        //public async Task CreateRangeAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Order order = TODO;

        //    // Act
        //    var result = await unitUnderTest.CreateRangeAsync(
        //        order);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public async Task UpdateRangeAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Order oldOrder = TODO;
        //    Order newOrder = TODO;

        //    // Act
        //    var result = await unitUnderTest.UpdateRangeAsync(
        //        oldOrder,
        //        newOrder);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public async Task CalculateAllProperties_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    LineItem item = TODO;
        //    Guid id = TODO;
        //    DateTime createTime = TODO;

        //    // Act
        //    await unitUnderTest.CalculateAllProperties(
        //        item,
        //        id,
        //        createTime);

        //    // Assert
        //    Assert.Fail();
        //}
    }
}
