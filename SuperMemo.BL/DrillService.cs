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
            return cardRepo.All().Where(card => card.NextDate < dateTime).ToList();
        }

        public void UpdateCard(Card card)
        {
            cardRepo.Update(card);
        }
    }
}
