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

        [TestMethod]
        public void ResponseRegisterSuccessTest()
        {
            int userId = 1;
            string userName = "userTest";
            string email = "email@test";
            string message = "Success on register.";
            ApiResponse.RegisterUserDto registerResponse = new ApiResponse.RegisterUserDto();

            registerResponse.RegisterSuccess(userId, userName, email);

            Assert.IsTrue(registerResponse.Success);
            Assert.AreEqual(userId, registerResponse.UserId);
            Assert.AreEqual(userName, registerResponse.UserName);
            Assert.AreEqual(email, registerResponse.Email);
            Assert.AreEqual(message, registerResponse.ResponseMessage);
        }

        [TestMethod]
        public void ResponseRegisterInvalidEmailTest()
        {
            string message = "This email is invalid.";
            ApiResponse.RegisterUserDto registerResponse = new ApiResponse.RegisterUserDto();

            registerResponse.InvalidEmail();

            Assert.IsFalse(registerResponse.Success);
            Assert.AreEqual(0, registerResponse.UserId);
            Assert.IsTrue(string.IsNullOrEmpty(registerResponse.UserName));
            Assert.IsTrue(string.IsNullOrEmpty(registerResponse.Email));
            Assert.AreEqual(message, registerResponse.ResponseMessage);
        }

        [TestMethod]
        public void ResponseRegisterExistentEmailTest()
        {
            string message = "This email already in use.";
            ApiResponse.RegisterUserDto registerResponse = new ApiResponse.RegisterUserDto();

            registerResponse.EmailInUse();

            Assert.IsFalse(registerResponse.Success);
            Assert.AreEqual(0, registerResponse.UserId);
            Assert.IsTrue(string.IsNullOrEmpty(registerResponse.UserName));
            Assert.IsTrue(string.IsNullOrEmpty(registerResponse.Email));
            Assert.AreEqual(message, registerResponse.ResponseMessage);
        }

        [TestMethod]
        public void ResponseRegisterExistentUserNameTest()
        {
            string message = "This user name already in use.";
            ApiResponse.RegisterUserDto registerResponse = new ApiResponse.RegisterUserDto();

            registerResponse.NameInUse();

            Assert.IsFalse(registerResponse.Success);
            Assert.AreEqual(0, registerResponse.UserId);
            Assert.IsTrue(string.IsNullOrEmpty(registerResponse.UserName));
            Assert.IsTrue(string.IsNullOrEmpty(registerResponse.Email));
            Assert.AreEqual(message, registerResponse.ResponseMessage);
        }
    }
}