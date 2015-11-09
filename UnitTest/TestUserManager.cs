using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.UserProvider;
using SCD_UVSS.Model;

namespace UnitTest
{
    public class TestUserManager
    {
        private UserManager ConstructUserManager()
        {
            var listUsers = new List<UserInfo>
            {
                new UserInfo() {Name = "jacob", Password = "pass"},
                new UserInfo() {Name = "admin", Password = "pass2"}
            };

            return new UserManager(new MockUserProvider(listUsers));
        }

        [Test]
        public void TestValidUser()
        {
            var userManager = this.ConstructUserManager();

            string errorMsg;
            var isValid = userManager.IsUserValid("jacob", "pass", out errorMsg);

            Assert.AreEqual(isValid, true);
        }

        [Test]
        public void TestInValidUser()
        {
            var userManager = this.ConstructUserManager();

            string errorMsg;
            var isValid = userManager.IsUserValid("jacob2", "pass", out errorMsg);

            Assert.AreEqual(isValid, false);
        }

        [Test]
        public void TestInValidPassword()
        {
            var userManager = this.ConstructUserManager();

            string errorMsg;
            var isValid = userManager.IsUserValid("jacob", "passB", out errorMsg);

            Assert.AreEqual(isValid, false);
        }
    }
}
