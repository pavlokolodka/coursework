using Bogus;
using DotNetEnv;
using ReserveSpot.Domain;

namespace DomainTests
{
    [TestClass]
    public class UserJSONDaoTests
    {
        public Faker faker;

        [TestInitialize]
        public void Initialize() {
            Env.TraversePath().Load();
            faker = new Faker("en");
        }

        [TestMethod]
        public void Create()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            var userDao = new UserJSONDao();
            userDao.Create(newUser);

            User savedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNotNull(savedUser);
            Assert.AreEqual(savedUser.Email, newUser.Email);
            Assert.AreEqual(savedUser.FirstName, newUser.FirstName);
            Assert.AreEqual(savedUser.LastName, newUser.LastName);
            Assert.IsNull(savedUser.UserCode);
            Assert.IsFalse(savedUser.IsAdmin);
            
            userDao.Delete(user => user.ID == newUser.ID);
        }

        [TestMethod]
        public void Create_With_Generated_User_Code()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            
            int generatedCode = newUser.GenerateUserCode();
            var userDao = new UserJSONDao();
            userDao.Create(newUser);

            User savedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNotNull(savedUser);
            Assert.AreEqual(savedUser.Email, newUser.Email);
            Assert.AreEqual(savedUser.FirstName, newUser.FirstName);
            Assert.AreEqual(savedUser.LastName, newUser.LastName);
            Assert.AreEqual(savedUser.UserCode, generatedCode);
            Assert.IsTrue(savedUser.IsValidUserCode());
            Assert.IsFalse(savedUser.IsAdmin);
       
            userDao.Delete(user => user.ID == newUser.ID);
        }

        [TestMethod]
        public void Create_Throws_Validation_Exception()
        {
            string email = "invalid_email";
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            var userDao = new UserJSONDao();

            try 
            {
              userDao.Create(newUser);
              Assert.Fail("Expected ValidationException was not thrown");
            } catch (Exception ex) { 
              Assert.AreEqual(ex.Message, "Email: Invalid email address");
              Assert.IsTrue(ex is System.ComponentModel.DataAnnotations.ValidationException);
            }
        }

        [TestMethod]
        public void Update()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            var userDao = new UserJSONDao();
            userDao.Create(newUser);

            User savedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNotNull(savedUser);

            string updatedFirstName = faker.Name.FirstName();
            savedUser.FirstName = updatedFirstName;
            userDao.Update(user => user.ID == newUser.ID, savedUser);

            User updatedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNotNull(updatedUser);

            Assert.AreEqual(updatedUser.Email, newUser.Email);
            Assert.AreEqual(updatedUser.FirstName, updatedFirstName);
            Assert.AreEqual(updatedUser.LastName, newUser.LastName);
            Assert.IsNull(savedUser.UserCode);
            Assert.IsFalse(savedUser.IsAdmin);
     
            userDao.Delete(user => user.ID == newUser.ID);
        }

        [TestMethod]
        public void Update_Throws_Validation_Exception()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            var userDao = new UserJSONDao();
            userDao.Create(newUser);
            User savedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNotNull(savedUser);                       

            try
            {
                savedUser.Email = "Invalid email";
                userDao.Update(user => user.ID == savedUser.ID, savedUser);
                Assert.Fail("Expected ValidationException was not thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Email: Invalid email address");
                Assert.IsTrue(ex is System.ComponentModel.DataAnnotations.ValidationException);
     
                userDao.Delete(user => user.ID == newUser.ID);
            }
        }


        [TestMethod]
        public void Delete()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            var userDao = new UserJSONDao();
            userDao.Create(newUser);

            User savedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNotNull(savedUser);
        
            bool flag = userDao.Delete(user => user.ID == newUser.ID);
            Assert.IsTrue(flag);

            var deletedUser = userDao.FindOne(user => user.ID == newUser.ID);
            Assert.IsNull(deletedUser);
        }


        [TestMethod]
        public void Find_All()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User newUser = new User(email, password, firstName, lastName);
            User newUser2 = new User(email, password, firstName, lastName);
            var userDao = new UserJSONDao();
            userDao.Create(newUser);
            userDao.Create(newUser);

            Guid[] userIds = { newUser.ID, newUser2.ID };

            List<User> savedUsers = userDao.FindMany(user => userIds.Contains(user.ID));
            Assert.IsTrue(savedUsers.Count == 2);
             
            userDao.Delete(user => user.ID == newUser.ID);
            userDao.Delete(user => user.ID == newUser2.ID);
        }



    }
}