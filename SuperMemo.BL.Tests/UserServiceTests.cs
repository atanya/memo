using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoRepository;
using NUnit.Framework;
using SuperMemo.DomainModel;

namespace SuperMemo.BL.Tests
{
    [TestFixture]
    public class MongoDate
    {
        [Test]
        public void TestFilter()
        {
            var cardRepo = new MongoRepository<Card>();

            var queryable = cardRepo.All();
            var dateTime = DateTime.UtcNow.Date.AddDays(4);
            var f1 = queryable.ToList().Where(card => card.NextDate < dateTime).ToList();
            var f2 = queryable.Where(card => card.NextDate < dateTime).ToList();
            Console.WriteLine("f1 count = {0}", f1.Count);
            Console.WriteLine("f2 count = {0}", f2.Count);
        }
    }

    [TestFixture]
    [Ignore]
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
