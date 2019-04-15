using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYardTests;

namespace BookStoreTests.Services
{
    [TestClass]
    public class BookServiceTests
    {
        private BookService service;

        [TestInitialize]
        public void TestInitialize()
        {
            var contextBuilder = new TestDbContextBuilder();
            contextBuilder.FillDb(CollectionsFactory.GetAuthorsCollection());
            contextBuilder.FillDb(CollectionsFactory.GetBooksCollection());
            contextBuilder.FillDb(CollectionsFactory.GetBookAuthorsCollection());
            contextBuilder.FillDb(CollectionsFactory.GetReviewsCollection());
            var context = contextBuilder.BuildContext();

            service = new BookService(context, new BookValidator());
        }

        [TestMethod]
        public async Task AllAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetBooksCollection().Count();

            // Act
            var result = await this.service.AllAsync();
            var realCount = result.Count();

            // Assert
            Assert.AreEqual(realCount, expectedCount, $"Expected - {expectedCount}, real - {realCount}");
        }

        // TODO add validations error test
        [TestMethod]
        public async Task SaveAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetBooksCollection().Count() + 1;
            var testBook = CollectionsFactory.GetBooksCollection().First();
            testBook.Id = new Guid();

            // Act
            var result = await this.service.SaveAsync(testBook);
            var newBooksCollection = await this.service.AllAsync();
            var newCount = newBooksCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBook = CollectionsFactory.GetBooksCollection().Where(b => b.Title == "Design Patterns via C#").First();
            var expectedAuthors = CollectionsFactory.GetBookAuthorsCollection()
                .Where(ba => ba.BookId == testBook.Id)
                .Count();

            var expectedReviews = CollectionsFactory.GetReviewsCollection()
                .Where(r => r.BookId == testBook.Id)
                .Count();

            // Act
            var book = await this.service.GetAsync(testBook.Id);
            var realBookAuthors = book.BookAuthors.Count();
            var realReviews = book.Reviews.Count();

            // Assert
            Assert.AreEqual(expectedAuthors, realBookAuthors, $"Expected - {expectedAuthors} authors, real - {realBookAuthors}");
            Assert.AreEqual(expectedReviews, realReviews, $"Expected - {expectedReviews} reviews, real - {realReviews}");
        }

        // TODO add validations error test
        [TestMethod]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBook = CollectionsFactory.GetBooksCollection().First();
            testBook.Title = "New Book";

            // Act
            var result = await this.service.UpdateAsync(testBook);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task RemoveAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetBooksCollection().Count() - 1;
            var testBook = CollectionsFactory.GetBooksCollection().First();

            // Act
            var result = await this.service.RemoveAsync(testBook.Id);
            var newBooksCollection = await this.service.AllAsync();
            var newCount = newBooksCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        // TODO add test
        //[TestMethod]
        //public IEnumerable<Author> SortAuthors_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange           

        //    // Act            

        //    // Assert
        //}

        // TODO add test
        //[TestMethod]
        //public async Task UpdateAuthorsAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange           

        //    // Act            

        //    // Assert
        //} 
        
        // TODO add test for discount
    }
}
