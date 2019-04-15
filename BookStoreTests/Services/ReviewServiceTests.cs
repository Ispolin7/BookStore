using BookStore.DataAccess;
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
        public async Task AllAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetReviewsCollection().Count();

            // Act
            var result = await this.service.AllAsync();
            var realCount = result.Count();

            // Assert
            Assert.AreEqual(realCount, expectedCount, $"Expected - {expectedCount}, real - {realCount}");
        }

        [TestMethod]
        public async Task AllBookReviewsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testReview = CollectionsFactory.GetReviewsCollection().First();

            // Act
            var result = await this.service.AllBookReviewsAsync(testReview.BookId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testReview = CollectionsFactory.GetReviewsCollection().First();

            // Act
            var result = await this.service.GetAsync(testReview.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        // TODO validation error
        [TestMethod]
        public async Task SaveAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetReviewsCollection().Count() + 1;
            var testReview = CollectionsFactory.GetReviewsCollection().First();
            testReview.Id = new Guid();

            // Act
            var result = await this.service.SaveAsync(testReview);
            var newReviewsCollection = await this.service.AllAsync();
            var newCount = newReviewsCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        [TestMethod]
        public async Task RemoveAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var expectedCount = CollectionsFactory.GetReviewsCollection().Count() - 1;
            var testAuthor = CollectionsFactory.GetReviewsCollection().First();

            // Act
            var result = await this.service.RemoveAsync(testAuthor.Id);
            var newReviewCollection = await this.service.AllAsync();
            var newCount = newReviewCollection.Count();

            // Assert
            Assert.AreEqual(newCount, expectedCount, $"Expected - {expectedCount}, real - {newCount}");
        }

        // TODO validation error
        [TestMethod]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testReview = CollectionsFactory.GetReviewsCollection().First();
            testReview.VoterName = "New Name";

            // Act
            var result = await this.service.UpdateAsync(testReview);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
