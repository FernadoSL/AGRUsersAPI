using AGRUsersAPI.Domain.Entities;
using AGRUsersAPI.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace AGRUsersAPI.Test.Entities
{
    [TestClass]
    public class UserLogTest
    {
        private UserLogFactory UserLogFactory { get; set; }

        [TestInitialize]
        public void InitUserLogTest()
        {
            this.UserLogFactory = new UserLogFactory();
        }

        [TestMethod]
        public void LogOnLoginTest()
        {
            UserLog userLog = this.UserLogFactory.Create(1);
            userLog.LogIn();
            
            Assert.AreEqual(this.TrimMilliSeconds(userLog.LoginDateTime), this.TrimMilliSeconds(DateTime.Now));
        }

        [TestMethod]
        public void LogOnLogoutTest()
        {
            UserLog userLog = this.UserLogFactory.Create(1);
            userLog.LogOut();

            Assert.IsTrue(userLog.LogoutDateTime.HasValue);
            Assert.AreEqual(this.TrimMilliSeconds(userLog.LogoutDateTime.Value), this.TrimMilliSeconds(DateTime.Now));
        }

        [TestMethod]
        public void UserLoggedTest()
        {
            UserLog userLog = this.UserLogFactory.Create(1);
            userLog.LogIn();

            Assert.IsTrue(userLog.IsLogged);
        }

        [TestMethod]
        public void UserNotLoggedTest()
        {
            UserLog userLog = this.UserLogFactory.Create(1);
            userLog.LogIn();
            userLog.LogOut();

            Assert.IsFalse(userLog.IsLogged);
        }

        [TestMethod]
        public void MinutesLoggedWithoutLogout()
        {
            UserLog userLog = this.UserLogFactory.Create(1);
            DateTime loginDateTime = DateTime.Now;
            userLog.LogIn();

            Thread.Sleep(60000);
            Assert.AreEqual(userLog.MinutesLogged, 1);
        }

        [TestMethod]
        public void MinutesLoggedWithLogout()
        {
            UserLog userLog = this.UserLogFactory.Create(1);
            DateTime loginDateTime = DateTime.Now;

            userLog.LogIn();
            Thread.Sleep(60000);
            userLog.LogOut();

            Assert.AreEqual(userLog.MinutesLogged, 1);
        }

        private DateTime TrimMilliSeconds(DateTime dateToTrim)
        {
            dateToTrim = dateToTrim.AddTicks(-dateToTrim.Ticks);

            return dateToTrim.AddMilliseconds(-dateToTrim.Millisecond);
        }
    }
}
