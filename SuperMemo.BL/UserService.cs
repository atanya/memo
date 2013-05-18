using System.Security.Cryptography;
using System.Text;
using MongoRepository;
using SuperMemo.BL.Entities;

namespace SuperMemo.BL
{
    public class UserService
    {
        private readonly IRepository<User> _userRepo;

        public UserService()
        {
            _userRepo = new MongoRepository<User>();
        }

        public User FindByNameAndPassword(string userName, string password)
        {
            var passwordHash = ComputeMd5Hash(userName, password);
            
            return _userRepo.GetSingle(_ => _.PasswordHash == passwordHash);
        }

        public User FindByHash(string hash)
        {
            return _userRepo.GetSingle(_ => _.PasswordHash == hash);
        }

        public void Create(string userName, string password)
        {
            var passwordHash = ComputeMd5Hash(userName, password);
            
            _userRepo.Add(new User {Name = userName, PasswordHash = passwordHash});
        }

        public static string ComputeMd5Hash(string userName, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(userName + password));
            
            var sBuilder = new StringBuilder();

            foreach (var b in hashBytes)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
