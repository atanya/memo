using System;

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
        private const double MinEF = 1.3;

        public static DateTime GetNextDate(Card card, Score score)
        {
            var newCard = UpdateCard(card, score);
            return newCard.NextTrainingDate;
        }

        public static int GetInterval(Card card, Score score)
        {
            var newCard = UpdateCard(card, score);
            return newCard.LastInterval;
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
            }
            else if (card.NumberOfRepetitions == 2)
            {
                card.LastInterval = SecondInterval;
            }
            else
            {
                //Calculate new EF
                card.EFactor = CalculateNextEFactor(card.EFactor, score);
                //Calculate and return new interval
                card.LastInterval = CalculateNextInterval(card.LastInterval, card.EFactor);
            }
            card.NextTrainingDate = DateTime.Now.Date.AddDays(card.LastInterval);
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
            //EF':=EF+(0.1-(5-q)*(0.08+(5-q)*0.02))
            var q = (int) score;
            var newEF = eFactor + (0.1 - (5 - q)*(0.08 + (5 - q)*0.02));
            return newEF < MinEF ? MinEF : newEF;
        }

        private static int CalculateNextInterval(int lastInterval, double eFactor)
        {
            return (int)Math.Ceiling(lastInterval * eFactor);
        }
    }
}
