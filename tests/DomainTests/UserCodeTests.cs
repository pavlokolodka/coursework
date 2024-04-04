using ReserveSpot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests
{

    [TestClass]
    public class UserCodeTests
    {
        [TestMethod]
        public void TestGenerateUserCode()
        {
            UserCode userCode = new UserCode();

            int generatedCode = userCode.GenerateUserCode();

            Assert.IsTrue(generatedCode >= 100000 && generatedCode < 1000000, "Generated code should be a 6-digit number");
            Assert.AreEqual(generatedCode, userCode.Code, "Generated code should be set in the Code property");
        }

        [TestMethod]
        public void TestValidateUserCode_Valid()
        {
            UserCode userCode = new UserCode();
            userCode.GenerateUserCode();
            userCode.UpdatedAt = DateTime.Now.AddHours(-12); // Updated 12 hours ago

            bool isValid = userCode.ValidateUserCode();

            Assert.IsTrue(isValid, "User code should be valid as it was updated less than 24 hours ago");
        }

        [TestMethod]
        public void TestValidateUserCode_Invalid()
        {
            UserCode userCode = new UserCode();
            userCode.GenerateUserCode();
            userCode.UpdatedAt = DateTime.Now.AddHours(-25);

            bool isValid = userCode.ValidateUserCode();

            Assert.IsFalse(isValid, "User code should be invalid as it was updated more than 24 hours ago");
        }
    }
}
