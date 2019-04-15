using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiYardTests;

namespace BookStoreTests.Services
{
    [TestClass]
    public class AuthorServiceTests
    {
        private IAuthorService service;
       

        [TestInitialize]
        public void TestInitialize()
        {
            var contextBuilder = new TestDbContextBuilder();
            contextBuilder.FillDb(CollectionsFactory.GetAuthorsCollection());
            contextBuilder.FillDb(CollectionsFactory.GetBooksCollection());
            contextBuilder.FillDb(CollectionsFactory.GetBookAuthorsCollection());

            service = new AuthorService(contextBuilder.BuildContext()); 
        }

        [TestMethod]
        public async Task AllAsync_GetAllAuthors_ExpectedEqualAmount()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetAuthorsCollection().Count();

            // Act
            var result = await this.service.AllAsync();
            var realCount = result.Count();

            // Assert
            Assert.AreEqual(realCount, expectedCount, $"Expected - {expectedCount}, real - {realCount}");
        }

        [TestMethod]
        public async Task GetAsync_GetAuthor_ExpectedEqualBooksAmount()
        {
            // Arrange
            var testAuthor = CollectionsFactory.GetAuthorsCollection().First();
            var expectedBooks = CollectionsFactory.GetBookAuthorsCollection()
                .Where(ba => ba.AuthorId == testAuthor.Id)
                .Count();

            // Act
            var author = await this.service.GetAsync(testAuthor.Id);
            var firstAuthorBooks = author.BookAuthors.Count();

            // Assert
            Assert.AreEqual(firstAuthorBooks, expectedBooks, $"Expected - {expectedBooks}, real - {firstAuthorBooks}");
        }

        [TestMethod]
        public async Task SaveAsync_AddNewAuthor_ExpectedIncrement()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetAuthorsCollection().Count() + 1;
            var testAuthor = CollectionsFactory.GetAuthorsCollection().First();
            testAuthor.Id = new Guid();

            // Act
            var result = await this.service.SaveAsync(testAuthor);
            var newAuthorsCollection = await this.service.AllAsync();
            var newCount = newAuthorsCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testAuthor = CollectionsFactory.GetAuthorsCollection().First();
            testAuthor.Name = "New Name";

            // Act
            var result = await this.service.UpdateAsync(testAuthor);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task RemoveAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetAuthorsCollection().Count() - 1;
            var testAuthor = CollectionsFactory.GetAuthorsCollection().First();

            // Act
            var result = await this.service.RemoveAsync(testAuthor.Id);
            var newAuthorsCollection = await this.service.AllAsync();
            var newCount = newAuthorsCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }        
    }
}
