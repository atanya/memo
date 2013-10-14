using System;
using System.Collections.Generic;
using System.Configuration;
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

        public string Save(string id, string word, string translation, User owner)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(translation) || owner == null)
            {
                throw new ArgumentException("Word, translation or user is null");
            }

            if (string.IsNullOrEmpty(id))
            {
                return Create(word, translation, owner);
            }
            else
            {
                var card = GetCardByID(id);
                if (card != null && card.Owner.Id == owner.Id)
                {
                    return Update(card, word, translation);
                }
                else
                {
                    return Create(word, translation, owner);
                }
            }
        }

        private string Create(string word, string translation, User owner)
        {
            var card = new Card {Word = word, Translation = translation, Owner = owner};
            card = Algorithm.InitCard(card);
            card = cardRepo.Add(card);
            return card.Id;
        }

        private string Update(Card card, string word,  string translation)
        {
            card.Word = word;
            card.Translation = translation;
            cardRepo.Update(card);
            return card.Id;
        }

        public List<Card> List(string ownerID)
        {
            return cardRepo.All(c => c.Owner.Id == ownerID).ToList();
        }
        
        //Restrict for admins
        public List<Card> ListAll()
        {
            return cardRepo.All().ToList();
        }

        public long Count(string ownerID)
        {
            return cardRepo.All(c => c.Owner.Id == ownerID).Count();
        }

        public void Delete(string id, string ownerID)
        {
            //var card = GetCardByContent(id);
            var card = cardRepo.GetById(id);
            if (card != null && card.Owner.Id == ownerID)
            {
                cardRepo.Delete(card);
            }
        }

        public Card GetCardByContent(string word, string ownerID)
        {
            return cardRepo.GetSingle(c => c.Word == word && c.Owner.Id == ownerID);
        }

        public Card GetCardByID(string id)
        {
            var card = cardRepo.GetById(id);
            return card;
        }

    }
}
