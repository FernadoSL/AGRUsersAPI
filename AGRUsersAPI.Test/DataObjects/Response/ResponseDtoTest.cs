using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiResponse = AGRUsersAPI.DataObjects.Reposnse;

namespace AGRUsersAPI.Test.DataObjects.Response
{
    [TestClass]
    public class ResponseDtoTest
    {
        [TestMethod]
        public void ResponseLoginSuccessTest()
        {
            int userId = 1;
            string userName = "test";
            string email = "test@test"; 
            string message = "Succes on login.";
            ApiResponse.LoginUserDto loginResponse = new ApiResponse.LoginUserDto();

            loginResponse.LoginSuccess(1, "test", "test@test");

            Assert.IsTrue(loginResponse.Success);
            Assert.AreEqual(userId, loginResponse.UserId);
            Assert.AreEqual(userName, loginResponse.UserName);
            Assert.AreEqual(email, loginResponse.Email);
            Assert.AreEqual(message, loginResponse.ResponseMessage);
        }

        [TestMethod]
        public void ResponseLoginFailTest()
        {
            string message = "Login or password invalid.";
            ApiResponse.LoginUserDto loginResponse = new ApiResponse.LoginUserDto();

            loginResponse.LoginFail();

            Assert.IsFalse(loginResponse.Success);
            Assert.AreEqual(0, loginResponse.UserId);
            Assert.IsTrue(string.IsNullOrEmpty(loginResponse.UserName));
            Assert.IsTrue(string.IsNullOrEmpty(loginResponse.Email));
            Assert.AreEqual(message, loginResponse.ResponseMessage);
        }
    }
}