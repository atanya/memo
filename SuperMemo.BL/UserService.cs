using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using MongoRepository;
using SuperMemo.DomainModel;

namespace SuperMemo.BL
{
    public class UserService
    {
        private readonly IRepository<User> _userRepo;

        public UserService()
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MONGOLAB_URI"); // for appharbor
            _userRepo = string.IsNullOrEmpty(connectionstring) ? new MongoRepository<User>() : new MongoRepository<User>(connectionstring);
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
        
        public void Update(string id, string password)
        {
            var user = _userRepo.GetById(id);
            user.PasswordHash = ComputeMd5Hash(user.Name, password);
            _userRepo.Update(user);
        }

        public User FindByName(string userName)
        {
            return _userRepo.GetSingle(_ => _.Name == userName);
        }

        private static string ComputeMd5Hash(string userName, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(userName.ToLower() + password));
            
            var sBuilder = new StringBuilder();

            foreach (var b in hashBytes)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
