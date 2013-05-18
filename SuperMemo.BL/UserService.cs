using System.Security.Cryptography;
using System.Text;
using MongoRepository;
using SuperMemo.DomainModel;

namespace SuperMemo.BL
{
    public class UserService
    {
        public User FindByNameAndPassword(string userName, string password)
        {
            var passwordHash = ComputeMd5Hash(userName, password);
            var userRepo = new MongoRepository<User>();
            return userRepo.GetSingle(_ => _.PasswordHash == passwordHash);
        }

        public void Create(string userName, string password)
        {
            var passwordHash = ComputeMd5Hash(userName, password);
            var userRepo = new MongoRepository<User>();
            userRepo.Add(new User {Name = userName, PasswordHash = passwordHash});
        }

        private static string ComputeMd5Hash(string userName, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(userName + password));
            return Encoding.UTF8.GetString(hashBytes);
        }
    }
}
