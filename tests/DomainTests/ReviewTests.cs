using ReserveSpot;

namespace DomainTests
{
    [TestClass]
    public class ReviewTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            int rating = 5;
            string comment = "Test Comment";
            string userId = "user123";
            string propertyId = "property123";

            Review review = new Review(rating, comment, userId, propertyId);

            Assert.AreEqual(rating, review.Rating);
            Assert.AreEqual(comment, review.Comment);
            Assert.AreEqual(userId, review.UserID);
            Assert.AreEqual(propertyId, review.PropertyID);
        }
    }
}
