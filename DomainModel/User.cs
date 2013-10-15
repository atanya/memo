using System.Collections.Generic;
using MongoRepository;

namespace SuperMemo.DomainModel
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        //public IList<Card> Cards { get; set; }
    }
}
