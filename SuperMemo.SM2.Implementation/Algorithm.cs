using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMemo.SM2.Implementation
{
    public enum Score
    {
        None = 0,
        Hard = 3,
        Medium = 4,
        Easy = 5
    }
    public class Card
    {
        public string Word;
        public string Translation;
        public DateTime? LastTrainingDate;
        public Score Score;
        public int LastInterval;
        public int NumberOfRepetitions;
        public double EFactor;
        public DateTime NextTrainingDate;
    }



    public static class Algorithm
    {
        private const int FirstInterval = 0;
        private const int SecondInterval = 3;

        public static DateTime GetNextDate(Card card, Score score)
        {
            return DateTime.Now;
        }

        public static int GetInterval(Card card, Score score)
        {
            return 1;
        }

        public static Card InitCard(Card card)
        {
            card.LastTrainingDate = null;
            card.Score = Score.None;
            card.LastInterval = 0;
            card.NumberOfRepetitions = 0;
            card.EFactor = 2.5;
            card.NextTrainingDate = DateTime.Now.Date;
            return card;
        }

        private static Card UpdateCard(Card card, Score score)
        {
            card.LastTrainingDate = DateTime.Now.Date;
            card.Score = score;
            card.NumberOfRepetitions++;
            //If None - return interval
            if (score == Score.None)
            {
                return ResetCard(card);
            }
            //If first or second time - return interval
            if (card.NumberOfRepetitions == 1)
            {
                card.LastInterval = FirstInterval;
                card.NextTrainingDate = DateTime.Now.Date.AddDays(card.LastInterval);
                return card;
            }
            if (card.NumberOfRepetitions == 2)
            {
                card.LastInterval = SecondInterval;
                card.NextTrainingDate = DateTime.Now.Date.AddDays(card.LastInterval);
                return card;
            }
            //Calculate new EF
            card.EFactor = CalculateNextEFactor(card.EFactor, score);
            //Calculate and return new interval
            card.LastInterval = CalculateNextInterval(card, score);
            return card;
        }

        private static Card ResetCard(Card card)
        {
            card.EFactor = 2.5;
            card.NumberOfRepetitions = 0;
            card.LastInterval = 0;
            card.NextTrainingDate = DateTime.Now.Date.AddDays(1);
            return card;
        }

        private static double CalculateNextEFactor(double eFactor, Score score)
        {
            return eFactor;
        }

        private static int CalculateNextInterval(Card card, Score score)
        {
            return 1;
        }
    }
}
