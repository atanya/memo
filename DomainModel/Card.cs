using System;
using MongoRepository;

namespace SuperMemo.DomainModel
{

    public enum Score
    {
        None = 0,
        Hard = 3,
        Medium = 4,
        Easy = 5
    }

    public class Card : Entity
    {
        protected bool Equals(Card other)
        {
            return string.Equals(Word, other.Word) && string.Equals(Translation, other.Translation) &&
                   LastTrainingDate.Equals(other.LastTrainingDate) && Score == other.Score &&
                   NextDate.Equals(other.NextDate) && EFactor.Equals(other.EFactor) &&
                   LastInterval == other.LastInterval && NumberOfRepetitions == other.NumberOfRepetitions;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Word != null ? Word.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Translation != null ? Translation.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ LastTrainingDate.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Score;
                hashCode = (hashCode*397) ^ NextDate.GetHashCode();
                hashCode = (hashCode*397) ^ EFactor.GetHashCode();
                hashCode = (hashCode*397) ^ LastInterval;
                hashCode = (hashCode*397) ^ NumberOfRepetitions;
                return hashCode;
            }
        }

        public string Word { get; set; }
        public string Translation { get; set; }
        public DateTime? LastTrainingDate { get; set; }
        public Score Score { get; set; }
        public DateTime NextDate { get; set; }
        public double EFactor { get; set; }
        public int LastInterval { get; set; }
        public int NumberOfRepetitions { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Card) obj);
        }


    }
}
