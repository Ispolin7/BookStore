using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BookStoreTests.Services
{
    [TestClass]
    public class BookServiceTests
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

        //private BookService CreateService()
        //{
        //    return new BookService(
        //        this.mockBookStoreContext.Object);
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
        //public async Task SaveAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateService();
        //    Book book = TODO;

        //    // Act
        //    var result = await unitUnderTest.SaveAsync(
        //        book);

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
        //    Book book = TODO;

        //    // Act
        //    var result = await unitUnderTest.UpdateAsync(
        //        book);

        //    // Assert
        //    Assert.Fail();
        //}
    }
}
