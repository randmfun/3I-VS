using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal.UserProvider;
using SCD_UVSS.Model;

namespace UnitTest
{
    public class TestBinaryUserProvider
    {
        private BinaryUserProvider constructBinaryUserProvider()
        {
            var binUserProvider = new BinaryUserProvider();
            binUserProvider.SetSavedUserInfos(new List<UserInfo>());
            return binUserProvider;
        }

        [Test]
        public void TestAddUser()
        {
            var binUserProvider = this.constructBinaryUserProvider();
            binUserProvider.AddUser(new UserInfo());
            Assert.AreEqual(binUserProvider.GetUsersList().Count, 1);
        }

        [Test]
        public void TestGetUsersList()
        {
            var binUserProvider = this.constructBinaryUserProvider();
            binUserProvider.AddUser(new UserInfo());
            binUserProvider.AddUser(new UserInfo());
            Assert.AreEqual(binUserProvider.GetUsersList().Count, 2);
        }

        [Test]
        public void TestSaveUsers()
        {
            var expectedNames = new List<string>(){"jacob", "admin", "op"};
            var expectedPasswords = new List<string>(){"password", "password", "password"};

            var binUserProvider = this.constructBinaryUserProvider();

            binUserProvider.AddUser(new UserInfo(){Name = expectedNames[0], Password = expectedPasswords[0]});
            binUserProvider.AddUser(new UserInfo(){Name = expectedNames[1], Role = Roles.Developer});

            // Save and Read together
            binUserProvider.SaveUsers();
            var usersList = binUserProvider.ReadSavedUserInfos();

            Assert.AreEqual(usersList[0].Name, expectedNames[0]);
            Assert.AreEqual(usersList[0].Password, expectedPasswords[1]);
            Assert.AreEqual(usersList[0].Role, Roles.Operator);

            Assert.AreEqual(usersList[1].Name, expectedNames[1]);
            Assert.AreEqual(usersList[1].Role, Roles.Developer);
            Assert.AreNotEqual(usersList[1].Role, Roles.Admin);
            Assert.AreNotEqual(usersList[1].Role, Roles.Operator);
        }
        
        //[Test]
        public void CreateASavedUserBinaryImage()
        {
            var binUserProvider = this.constructBinaryUserProvider();

            binUserProvider.AddUser(new UserInfo { Name = "super", Password = "JamesBond007", Role = Roles.Developer});
            binUserProvider.AddUser(new UserInfo { Name = "admin", Password = "Admin123", Role = Roles.Admin });
            binUserProvider.AddUser(new UserInfo { Name = "OperatorOne", Password = "Operator123", Role = Roles.Operator });
            binUserProvider.AddUser(new UserInfo { Name = "OperatorTwo", Password = "Operator123", Role = Roles.Operator });
            binUserProvider.AddUser(new UserInfo { Name = "OperatorThree", Password = "Operator123", Role = Roles.Operator });
            binUserProvider.AddUser(new UserInfo { Name = "OperatorFour", Password = "Operator123", Role = Roles.Operator });

            // Save
            binUserProvider.SaveUsers();
        }

        [Test]
        public void TestUpdatePasswordInMemory()
        {
            const string expectedUser = "super";
            const string expectedOldPassword = "JamesBond007";
            const string expectedNewPassword = "newPassword";

            var binUserProvider = this.constructBinaryUserProvider();
            binUserProvider.AddUser(new UserInfo { Name = expectedUser, Password = expectedOldPassword, Role = Roles.Developer });

            binUserProvider.UpdatePassword(expectedUser, expectedNewPassword);

            var users = binUserProvider.GetUsersList();
            Assert.IsTrue(users.Any(item => (item.Name == expectedUser && item.Password == expectedNewPassword)));
            Assert.IsFalse(users.Any(item => (item.Name == expectedUser && item.Password == expectedOldPassword)));
        }

        [Test]
        public void TestUpdatePasswordSavedUser()
        {
            const string expectedUser = "super";
            const string expectedOldPassword = "JamesBond007";
            const string expectedNewPassword = "newPassword";

            var binUserProvider = this.constructBinaryUserProvider();
            binUserProvider.AddUser(new UserInfo { Name = expectedUser, Password = expectedOldPassword, Role = Roles.Developer });

            binUserProvider.UpdatePassword(expectedUser, expectedNewPassword);
            binUserProvider.SaveUsers();

            var users = binUserProvider.ReadSavedUserInfos();
            Assert.IsTrue(users.Any(item => (item.Name == expectedUser && item.Password == expectedNewPassword)));
            Assert.IsFalse(users.Any(item => (item.Name == expectedUser && item.Password == expectedOldPassword)));
        }

        [Test]
        public void TestUpdateUserInfo()
        {
            const string expectedUser = "super";
            const string expectedNewUser = "superNew";
            const string expectedOldPassword = "JamesBond007";
            const string expectedNewPassword = "newPassword";

            var binUserProvider = this.constructBinaryUserProvider();
            var userInfo = new UserInfo {Name = expectedUser, Password = expectedOldPassword, Role = Roles.Developer};
            binUserProvider.AddUser(userInfo);

            binUserProvider.UpdateUserInfo(userInfo, expectedNewUser, expectedNewPassword);

            var users = binUserProvider.GetUsersList();
            Assert.IsTrue(users.Any(item => (item.Name == expectedNewUser && item.Password == expectedNewPassword)));
            Assert.IsFalse(users.Any(item => (item.Name == expectedNewUser && item.Password == expectedOldPassword)));
        }
    }
}
