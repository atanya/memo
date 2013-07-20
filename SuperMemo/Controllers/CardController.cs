using System.Web.Mvc;
using SuperMemo.BL;
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
            ITranslator t = new Translator();
            var translation = t.Translate(word);
            var viewModel = new CardViewModel {Word = word, Translation = translation};
            return View("EditCard", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CardViewModel card)
        {
            var cardService = new CardService();
            cardService.Create(card.Word, card.Translation);
            return Json("success");
        }
    }
}
