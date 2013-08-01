using System.Collections.Generic;
using System.Web.Mvc;
using SuperMemo.BL;
using SuperMemo.DomainModel;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    public class CardController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View("EditCard", new CardViewModel());
        }

        [HttpGet]
        public ActionResult Edit(string word)
        {
            //ITranslator t = new Translator();
            //var translation = t.Translate(word);
            var cardService = new CardService();
            var card = cardService.GetCardByContent(word);
            if (card == null)
            {
                return RedirectToAction("Create");
            }
            var viewModel = new CardViewModel {Word = word, Translation = card.Translation};
            return View("EditCard", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CardViewModel card)
        {
            var cardService = new CardService();
            cardService.Save(card.Word, card.Translation);
            return Json("success");
        }

        [HttpGet]
        public ActionResult List()
        {
            var cardService = new CardService();
            var list = cardService.List();
            var model = new CardListViewModel {Cards = list ?? new List<Card>()};
            return View("List", model);
        }

        [HttpPost]
        public ActionResult Delete(string word)
        {
            var cardService = new CardService();
            cardService.Delete(word);
            return Json("success");
        }
    }
}
