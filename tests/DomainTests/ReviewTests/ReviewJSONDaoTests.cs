using Bogus;
using ReserveSpot.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DomainTests
{
    [TestClass]
    public class ReviewJSONDaoTests
    {
        public Faker faker = new Faker("en");

        [TestMethod]
        public void Create()
        {
            int rating = faker.Random.Number(1, 10);
            string comment = faker.Lorem.Text();
            Guid userId = faker.Random.Uuid();
            Guid propertyId = faker.Random.Uuid(); 

            Review newReview = new Review(rating, comment, userId, propertyId);
            var dao = new ReviewJSONDao();
            dao.Create(newReview);

            var createdReview = dao.FindOne(review => review.ID == newReview.ID);
            Assert.IsNotNull(createdReview);
            Assert.AreEqual(rating, createdReview.Rating);
            Assert.AreEqual(comment, createdReview.Comment);
            Assert.AreEqual(userId, createdReview.UserID);
            Assert.AreEqual(propertyId, createdReview.PropertyID);
            
            dao.Delete(review => review.ID == newReview.ID);
        }   
        
        [TestMethod]
        public void Create_Throws_Validation_Exception()
        {
            Guid userId = faker.Random.Uuid();
            Guid propertyId = faker.Random.Uuid();
            int invalidRating = 11;
            int invalidCommentLength = 301;
            string comment = faker.Lorem.Sentence(invalidCommentLength);

            Review newReview = new Review(invalidRating, comment, userId, propertyId);
            var dao = new ReviewJSONDao();

            try
            {
                dao.Create(newReview);
                Assert.Fail("Expected ValidationException was not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is System.ComponentModel.DataAnnotations.ValidationException);
                dao.Delete(review => review.ID == newReview.ID);
            }
        }

        [TestMethod]
        public void Update_Throws_Validation_Exception()
        {
            int rating = faker.Random.Number(1, 10);
            string comment = faker.Lorem.Text();
            Guid userId = faker.Random.Uuid();
            Guid propertyId = faker.Random.Uuid();
            Review newReview = new Review(rating, comment, userId, propertyId);
            var dao = new ReviewJSONDao();

            var review = dao.Create(newReview);

            int invalidCommentLength = 301;
            review.Comment = faker.Lorem.Sentence(invalidCommentLength);            

            try
            {
                dao.Update(review => review.ID == newReview.ID, review);
                Assert.Fail("Expected ValidationException was not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is System.ComponentModel.DataAnnotations.ValidationException);
                dao.Delete(review => review.ID == newReview.ID);
            }
        }     
    }
}
