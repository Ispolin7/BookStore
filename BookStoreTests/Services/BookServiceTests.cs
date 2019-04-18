using BookStore.Common;
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

            service = new BookService(context, new BookValidator(), new BookAuthorValidator(context));
        }

        [TestMethod]
        public async Task AllAsync_GetAllBooksFromDB_ExpectedSameCount()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetBooksCollection().Count();

            // Act
            var result = await this.service.AllAsync();
            var realCount = result.Count();

            // Assert
            Assert.AreEqual(realCount, expectedCount, $"Expected - {expectedCount}, real - {realCount}");
        }

        [TestMethod]
        public async Task SaveAsync_AddNewBookInDb_ExpectedIncrement()
        {
            // Arrange
            var expectedCount = this.CountBooksInCollection() + 1;
            var testBook = this.GetTestBook();
            testBook.Id = new Guid();

            // Act
            var result = await this.service.SaveAsync(testBook);
            var newBooksCollection = await this.service.AllAsync();
            var newCount = newBooksCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task SaveAsync_AddNewBookInDb_ExpectedValidationException()
        {
            // Arrange                 
            var testOrder = this.GetTestBook();
            testOrder.ActualPrice = testOrder.OrgPrice + 1;

            // Act
            await service.SaveAsync(testOrder);
        }

        [TestMethod]
        public async Task GetAsync_GetBookInformation_ExpectedCorrectNumberOfDependencies()
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

        // TODO Exception The instance of entity type 'Book' cannot be tracked because another instance with the key value 
        [TestMethod]
        public async Task UpdateAsync_UpdateBookInfoInDb_ExpectedSuccess()
        {
            // Arrange
            var testBook = this.GetTestBook();
            testBook.Title = "New Book Title";

            // Act
            var result = await this.service.UpdateAsync(testBook);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task RemoveAsync_RemoveBookFromDb_ExpectedDecrementAmount()
        {
            // Arrange
            var expectedCount = this.CountBooksInCollection() - 1;
            var testBook = this.GetTestBook();

            // Act
            var result = await this.service.RemoveAsync(testBook.Id);
            var newBooksCollection = await this.service.AllAsync();
            var newCount = newBooksCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        public void SortAuthors_StateUnderTest_ExpectedBehavior()
        {
            var notOneAuthor = CollectionsFactory.GetBookAuthorsCollection().Where(ba => ba.Order == 1).First();
            var authorsCollection = CollectionsFactory.GetBookAuthorsCollection()
                .Where(ba => ba.BookId == notOneAuthor.BookId)
                .ToList();

            authorsCollection.Reverse();
            authorsCollection.ForEach(
                ba => ba.Author = CollectionsFactory.GetAuthorsCollection()
                .Where(a => a.Id == ba.AuthorId)
                .First()
            );

            // Act            
            var sortedAuthors = this.service.SortAutors(authorsCollection);

            // Assert
            Assert.IsTrue(sortedAuthors.First().Id == authorsCollection.Where(ba => ba.Order == 0).First().AuthorId);
        }

        //TODO validation error test
        //TODO create test
        [TestMethod]
        public async Task UpdateAuthorsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange           

            // Act            

            // Assert
        }

        // TODO add test for discount
        // TODO validation error
        [TestMethod]
        public async Task UpdateDiscountAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange           
            var booksCollection = CollectionsFactory.GetBooksCollection();
            // Act            

            // Assert
        }


        /// <summary>
        /// Get Book instance for test.
        /// </summary>
        /// <returns>book instance</returns>
        public Book GetTestBook()
        {
            return CollectionsFactory.GetBooksCollection().First();
        }

        /// <summary>
        /// Count the number of books.
        /// </summary>
        /// <returns></returns>
        public int CountBooksInCollection()
        {
            return CollectionsFactory.GetBooksCollection().Count();
        }
    }
}
