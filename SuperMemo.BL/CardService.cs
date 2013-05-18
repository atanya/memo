using System.Collections.Generic;
using System.Linq;
using SuperMemo.DomainModel;
using SuperMemo.SM2.Implementation;
using MongoRepository;

namespace SuperMemo.BL
{
    public class CardService
    {
        public void Create(string word, string translation)
        {
            var card = new Card {Word = word, Translation = translation};
            card = Algorithm.InitCard(card);
            var cardRepo = new MongoRepository<Card>();
            cardRepo.Add(card);
            }

        public List<Card> List()
        {
            var cardRepo = new MongoRepository<Card>();
            return cardRepo.All().ToList();
        }

        public void Delete(Card card)
        {

        }
    }
}
