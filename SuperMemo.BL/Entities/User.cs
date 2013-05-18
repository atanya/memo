using System.Collections.Generic;
using MongoRepository;

namespace SuperMemo.BL.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public IList<Card> Cards { get; set; }
    }
}
