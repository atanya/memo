using System;
using System.Collections.Generic;
using System.Web.Http;
using DTO;
using SuperMemo.BL;
using SuperMemo.Filters;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    [Authorize]
    [ErrorHandler]
    public class MainController : ApiController
    {
        // GET api/<controller> - return list of cards
        public ResponseObject Get()
        {
            var currentUser = new UserService().FindByName(User.Identity.Name);
            var cardService = new CardService();
            var list = cardService.List(currentUser.Id);
            List<CardDto> result = list.ConvertAll(c => new CardDto(c));
            return ResponseObject.Success(result);
        }

        // GET api/<controller>/5 - return card by id
        public ResponseObject Get(string id)
        {
            var card = new CardService().GetCardByID(id);
            return ResponseObject.Success(new CardDto(card));
        }

        // POST api/<controller> - update card
        public ResponseObject Post([FromBody]CardDto card)
        {
            if (card == null)
                throw new ArgumentException("Wrong parameter");
            var currentUser = new UserService().FindByName(User.Identity.Name);
            var id = new CardService().Save(card.ID, card.Word, card.Translation, currentUser);
            return ResponseObject.Success(id);
        }

        //PUT - update card
        //public long Put(string id, [FromBody]CardDto card)
        //{
        //    var currentUser = new UserService().FindByName(User.Identity.Name);
        //    new CardService().Save(card.ID, card.Word, card.Translation, currentUser);
        //    return 0;
        //}

        // DELETE api/<controller>/5 - delete card
        public void Delete(string id)
        {
            var currentUser = new UserService().FindByName(User.Identity.Name);
            new CardService().Delete(id, currentUser.Id);
        }
    }

    
}