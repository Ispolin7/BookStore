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
            var expectedCount = this.CountAuthorsInCollection();

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
            var testAuthor = GetTestAuthor();
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
            var expectedCount = CountAuthorsInCollection() + 1;
            var testAuthor = GetTestAuthor();
            testAuthor.Id = new Guid();

            // Act
            var result = await this.service.SaveAsync(testAuthor);
            var newAuthorsCollection = await this.service.AllAsync();
            var newCount = newAuthorsCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        // TODO System.InvalidOperationException: The instance of entity type 'Author' cannot be tracked because another instance with the key value '{Id: df48360c-7d42-4056-6d4a-08d6c0d61f8d}' is already being tracked. 
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testAuthor = GetTestAuthor();
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
            var expectedCount = CountAuthorsInCollection() - 1;
            var testAuthor = GetTestAuthor();

            // Act
            var result = await this.service.RemoveAsync(testAuthor.Id);
            var newAuthorsCollection = await this.service.AllAsync();
            var newCount = newAuthorsCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        /// <summary>
        /// Get Author instance for test.
        /// </summary>
        /// <returns>author instance</returns>
        public Author GetTestAuthor()
        {
            return CollectionsFactory.GetAuthorsCollection().First();
        }

        /// <summary>
        /// Count the number of authors.
        /// </summary>
        /// <returns></returns>
        public int CountAuthorsInCollection()
        {
            return CollectionsFactory.GetAuthorsCollection().Count();
        }
    }
}
