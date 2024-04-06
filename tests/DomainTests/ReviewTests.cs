using Bogus;
using ReserveSpot;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DomainTests
{
    [TestClass]
    public class ReviewTests
    {
        public Faker faker = new Faker("en");

        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            int rating = faker.Random.Number(0, 10);
            string comment = faker.Lorem.Text();
            Guid userId = faker.Random.Uuid();
            Guid propertyId = faker.Random.Uuid(); 

            Review review = new Review(rating, comment, userId, propertyId);

            Assert.AreEqual(rating, review.Rating);
            Assert.AreEqual(comment, review.Comment);
            Assert.AreEqual(userId, review.UserID);
            Assert.AreEqual(propertyId, review.PropertyID);
        }

        [TestMethod]
        public void TestReviewEdit()
        {
            Review review = new Review(5, "Old comment", faker.Random.Uuid(), faker.Random.Uuid());

            review.Edit(8, "New comment");

            Assert.AreEqual(8, review.Rating);
            Assert.AreEqual("New comment", review.Comment);
        }

        [TestMethod]
        public void TestPartialEdit()
        {
            Review review = new Review(5, "Test comment", faker.Random.Uuid(), faker.Random.Uuid());

            string comment = faker.Lorem.Text();
            int rating = faker.Random.Number(0, 10);

            review.Edit(rating, null);
            Assert.AreEqual(rating, review.Rating);
            Assert.AreNotEqual(comment, review.Comment);

            review.Edit(null, comment);
            Assert.AreEqual(rating, review.Rating);
            Assert.AreEqual(comment, review.Comment);            
        }

        [TestMethod]
        public void TestReviewConstructor_InvalidParams()
        {
            Guid userId = faker.Random.Uuid();
            Guid propertyId = faker.Random.Uuid();
            int invalidRating = 11;
            int invalidCommentLength = 301;
            string comment = faker.Lorem.Sentence(invalidCommentLength);

            var review = new Review(invalidRating, comment, userId, propertyId);
            var context = new ValidationContext(review, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(review, context, results, true);

            Assert.IsFalse(isValid);

            var expectedErrors = new Dictionary<string, string>
            {
                { nameof(Review.PropertyID), "PropertyID is required" },
                { nameof(Review.UserID), "UserID is required" },
                { nameof(Review.Rating), "Rating must be between 1 and 10" },
                { nameof(Review.Comment), "Comment must be a string with a maximum length of 300" }
            };

            foreach (var result in results)
            {
                foreach (var memberName in result.MemberNames)
                {
                   Assert.AreEqual(expectedErrors[memberName], result.ErrorMessage);                   
                }
            }       
        }

        [TestMethod]
        public void TestReviewEdit_InvalidParams()
        {
            Review review = new Review(5, "Old comment", faker.Random.Uuid(), faker.Random.Uuid());
                
            int invalidRating = 11;
            int invalidCommentLength = 301;
            string comment = faker.Lorem.Sentence(invalidCommentLength);

            review.Edit(invalidRating, comment);

            var context = new ValidationContext(review, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(review, context, results, true);

            Assert.IsFalse(isValid);

            var expectedErrors = new Dictionary<string, string>
            {
                { nameof(Review.Rating), "Rating must be between 1 and 10" },
                { nameof(Review.Comment), "Comment must be a string with a maximum length of 300" }
            };

            foreach (var result in results)
            {
                foreach (var memberName in result.MemberNames)
                {
                    Assert.AreEqual(expectedErrors[memberName], result.ErrorMessage);
                }
            }
        }
    }
}
