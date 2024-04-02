using ReserveSpot;

namespace DomainTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            string username = "testuser";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";

            User user = new User(username, password, firstName, lastName);

            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
            Assert.IsFalse(user.IsAdmin); 
        }

        [TestMethod]
        public void ComparePassword_ReturnsTrueWhenPasswordsMatch()
        {
            string username = "testuser";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";
            User user = new User(username, password, firstName, lastName);

            bool result = user.ComparePassword(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ComparePassword_ReturnsFalseWhenPasswordsDoNotMatch()
        {
            string username = "testuser";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";
            User user = new User(username, password, firstName, lastName);

            bool result = user.ComparePassword("wrongpassword");

            Assert.IsFalse(result);
        }
    }
}