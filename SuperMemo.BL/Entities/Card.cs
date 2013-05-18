using System;
using MongoRepository;

namespace SuperMemo.BL.Entities
{
    public class Card : Entity
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public DateTime? LastTrainingDate { get; set; }
        public int Score { get; set; }
        public DateTime NextDate { get; set; }
        public float EFactor { get; set; }
    }
}
