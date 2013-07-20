using System.Collections.Generic;
using System.Linq;
using SuperMemo.DomainModel;
using SuperMemo.SM2.Implementation;
using MongoRepository;

namespace SuperMemo.BL
{
    public class CardService
    {
        private MongoRepository<Card> cardRepo;

        public CardService()
        {
            cardRepo = new MongoRepository<Card>();
        }

        public void Save(string word, string translation)
        {
            var card = GetCardByContent(word);
            if (card == null)
            {
                Create(word, translation);
            }
            else
            {
                Update(card, translation);
            }
        }

        private void Create(string word, string translation)
        {
            var card = new Card {Word = word, Translation = translation};
            card = Algorithm.InitCard(card);
            cardRepo.Add(card);
        }

        private void Update(Card card, string translation)
        {
            card.Translation = translation;
            cardRepo.Update(card);
        }

        public List<Card> List()
        {
            return cardRepo.All().ToList();
        }

        public void Delete(string word)
        {
             var card = GetCardByContent(word);
            if (card != null)
            {
                cardRepo.Delete(card);
            }
        }

        private Card GetCardByContent(string word)
        {
            return cardRepo.GetSingle(c => c.Word == word);
        }
    }
}
