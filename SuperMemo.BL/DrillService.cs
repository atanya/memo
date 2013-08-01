using System;
using System.Collections.Generic;
using System.Linq;
using MongoRepository;
using SuperMemo.DomainModel;

namespace SuperMemo.BL
{
    public class DrillService
    {
        private MongoRepository<Card> cardRepo;

        public DrillService()
        {
            cardRepo = new MongoRepository<Card>();
        }

        public List<Card> GetCardsForDrill()
        {
            var dateTime = DateTime.UtcNow.Date.AddDays(1);
            //TODO: need to check how to remove first ToList - it seems that date in Mongo is compared incorrectly
            return cardRepo.All().ToList().Where(card => card.NextDate < dateTime).ToList();
        }

        public void UpdateCard(Card card)
        {
            cardRepo.Update(card);
        }
    }
}
