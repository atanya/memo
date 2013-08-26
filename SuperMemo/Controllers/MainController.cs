using System.Collections.Generic;
using System.Web.Http;
using DTO;
using SuperMemo.BL;
using SuperMemo.DomainModel;

namespace SuperMemo.Controllers
{
    public class ResponseObject
    {
        public string Data { get; set; }
    }
    public class MainController : ApiController
    {
        // GET api/<controller> - return list of cards
        public List<CardDto> Get()
        {
            var cardService = new CardService();
            var list = cardService.List();
            List<CardDto> result = list.ConvertAll(c => new CardDto(c));
            return result;
        }

        // GET api/<controller>/5 - return card by id
        public CardDto Get(string id)
        {
            var card = new CardService().GetCardByID(id);
            return new CardDto(card);
        }

        // POST api/<controller> - update card
        public long Post([FromBody]Card card)
        {
            new CardService().Save(card.Word, card.Translation);
            return 0;
        }

        //PUT - update card
        public long Put(string id, [FromBody]Card card)
        {
            new CardService().Save(card.Word, card.Translation);
            return 0;
        }

        // DELETE api/<controller>/5 - delete card
        public void Delete(string id)
        {
            new CardService().Delete(id);
        }
    }
}