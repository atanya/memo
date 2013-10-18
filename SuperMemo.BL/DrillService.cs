using System;
using System.Collections.Generic;
using System.Configuration;
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
            var connectionstring = ConfigurationManager.AppSettings.Get("MONGOLAB_URI"); // for appharbor
            cardRepo = string.IsNullOrEmpty(connectionstring) ? new MongoRepository<Card>() : new MongoRepository<Card>(connectionstring);
        }

        public List<Card> GetCardsForDrill(string ownerID)
        {
            var dateTime = DateTime.UtcNow.Date.AddDays(1);
            return cardRepo.All().Where(card => card.NextDate < dateTime && card.Owner.Id == ownerID).OrderBy(c => c.LastTrainingDate).ToList();
        }

        public void UpdateCard(Card card, string ownerID)
        {
            if (card.Owner.Id == ownerID)
            {
                cardRepo.Update(card);
            }
        }
    }
}
