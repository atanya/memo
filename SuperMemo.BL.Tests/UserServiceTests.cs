using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using NUnit.Framework;

namespace SuperMemo.BL.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private void DropDB()
        {
            var url = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString);
            var client = new MongoClient(url);
            client.GetServer().DropDatabase(url.DatabaseName);
        }

        [SetUp]
        public void TestSetup()
        {
            this.DropDB();
        }

        [TearDown]
        public void TestCleanup()
        {
            this.DropDB();
        }

        [Test]
        public void UserCreateTest()
        {
            var service = new UserService();

            const string userName = "test user";
            const string password = "qwerty";

            service.Create(userName, password);
            var user = service.FindByNameAndPassword(userName, password);
            Assert.IsNotNull(user);
            Assert.AreEqual(userName, user.Name);
        }
    }
}
