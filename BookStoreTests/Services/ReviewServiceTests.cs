using BookStore.Common;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiYardTests;

namespace BookStoreTests.Services
{
    [TestClass]
    public class ReviewServiceTests
    {
        private ReviewService service;

        [TestInitialize]
        public void TestInitialize()
        {
            var contextBuilder = new TestDbContextBuilder();
            contextBuilder.FillDb(CollectionsFactory.GetBooksCollection());
            contextBuilder.FillDb(CollectionsFactory.GetReviewsCollection());
            var context = contextBuilder.BuildContext();
            service = new ReviewService(context, new ReviewValidator(context));
        }

        [TestMethod]
        public async Task AllAsync_GetAllReviews_ExpectedCorrectAmoint()
        {
            // Arrange
            var expectedCount = this.CountReviewsInCollection();

            // Act
            var result = await this.service.AllAsync();
            var realCount = result.Count();

            // Assert
            Assert.AreEqual(realCount, expectedCount, $"Expected - {expectedCount}, real - {realCount}");
        }

        [TestMethod]
        public async Task AllBookReviewsAsync_GatAllBookReview_ExpectedNotNullCollection()
        {
            // Arrange
            var testReview = this.GetTestReview();

            // Act
            var result = await this.service.AllBookReviewsAsync(testReview.BookId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAsync_ReviewInformation_ExpectedNotNull()
        {
            // Arrange
            var testReview = this.GetTestReview();

            // Act
            var result = await this.service.GetAsync(testReview.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SaveAsync_AddNewReviewInDb_ExpectedIncrementAmount()
        {
            // Arrange
            var expectedCount = this.CountReviewsInCollection() + 1;
            var testReview = this.GetTestReview();
            testReview.Id = new Guid();

            // Act
            var result = await this.service.SaveAsync(testReview);
            var newReviewsCollection = await this.service.AllAsync();
            var newCount = newReviewsCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task SaveAsync_AddNewReviewInDb_ExpectedValidationException()
        {
            // Arrange           
            var testReview = this.GetTestReview();
            testReview.BookId = new Guid();

            // Act
            await this.service.SaveAsync(testReview);
        }

        [TestMethod]
        public async Task RemoveAsync_DeleteReviewInDb_ExpectedDecrementAmount()
        {
            // Arrange
            var expectedCount = this.CountReviewsInCollection() - 1;
            var testAuthor = this.GetTestReview();

            // Act
            var result = await this.service.RemoveAsync(testAuthor.Id);
            var newReviewCollection = await this.service.AllAsync();
            var newCount = newReviewCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        public async Task UpdateAsync_UpdateReviewInformation_ExpectedSuccess()
        {
            // Arrange
            var testReview = this.GetTestReview();
            testReview.VoterName = "New Name";

            // Act
            var result = await this.service.UpdateAsync(testReview);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Get Review instance for test.
        /// </summary>
        /// <returns>Review instance</returns>
        public Review GetTestReview()
        {
            return CollectionsFactory.GetReviewsCollection().First();
        }

        /// <summary>
        /// Count the number of reviews.
        /// </summary>
        /// <returns></returns>
        public int CountReviewsInCollection()
        {
            return CollectionsFactory.GetReviewsCollection().Count();
        }
    }
}
