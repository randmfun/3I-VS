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

            binUserProvider.AddUser(new UserInfo() { Name = "super", Password = "JamesBond007", Role = Roles.Developer});
            binUserProvider.AddUser(new UserInfo() { Name = "admin", Password = "Admin123", Role = Roles.Admin });
            binUserProvider.AddUser(new UserInfo() { Name = "OperatorDavid", Password = "Operator123", Role = Roles.Operator });

            // Save and Read together
            binUserProvider.SaveUsers();
            var usersList = binUserProvider.ReadSavedUserInfos();
        }
    }
}
