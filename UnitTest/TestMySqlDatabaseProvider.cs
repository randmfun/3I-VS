using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;

namespace UnitTest
{
    [TestFixture]
    //[Ignore("Ignore a MySqlLocalTest")]
    public class TestMySqlDatabaseProvider
    {
        const string ValidConnectionString = "SERVER=localhost;DATABASE=scduvss;UID=root;PASSWORD=Welcome@123;";

        [Test]
        public void TestOpenConnectionValid()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };
            Assert.IsTrue(mySqlProvider.Open(), "Test MySql Connection Failed");
        }

        [Test]
        public void TestOpenConnectionInValid()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = "SERVER=localhost;DATABASE=scduvss;UID=root;PASSWORD=123;"
            };
            Assert.False(mySqlProvider.Open(), "Test MySql Connection Failed");
        }
        
        [Test]
        public void TestCloseConnection()
        {
            var mySqlProvider = new MySqlDatabaseProvider();
            Assert.IsFalse(mySqlProvider.Close());
        }

        [Test]
        public void TestAddBlackListItem()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            Assert.IsTrue(mySqlProvider.AddBlackListItem(new BlackListItem("TN 01 1235")));
        }

        [Test]
        public void TestGetBlackListItem()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            var blackListItems = mySqlProvider.GetAllBlackListItem();
        }

    }
}
