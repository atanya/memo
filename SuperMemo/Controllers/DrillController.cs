using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperMemo.BL;
using SuperMemo.DomainModel;
using SuperMemo.Models;
using SuperMemo.SM2.Implementation;

namespace SuperMemo.Controllers
{
    public class DrillController : Controller
    {
        private const string CurrentCardKey = "CurrentCard";
        //
        // GET: /Drill/

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult GetNextWord(byte? answer)
        {
            if (answer.HasValue)
            {
                ProcessCurrentCard(answer.Value);
            }

            var cardService = new CardService();
            var total = cardService.Count();

            var drillService = new DrillService();
            var cards = drillService.GetCardsForDrill();
            Card currentCard = new Card();
            if (cards.Count > 0)
            {
                currentCard = cards[0];
                SaveToSession(currentCard);
            }
            

            var model = new DrillViewModel
                {
                    Word = currentCard.Word,
                    Translation = currentCard.Translation,
                    TotalWords = total,
                    LeftWords = cards.Count
                };
            return Json(model);
        }

        private void SaveToSession(Card card)
        {
            Session[CurrentCardKey] = card;
        }

        private void ProcessCurrentCard(byte answer)
        {
            var currentCard = Session[CurrentCardKey] as Card;
            if (currentCard == null)
            {
                return;
            }

            var cardFromService = new CardService().GetCardByContent(currentCard.Word);
            if (cardFromService == null || !cardFromService.Equals(currentCard))
            {
                return;
            }

            var score = Score.None;
            if (answer > 2 && answer < 6)
            {
                score = (Score)answer;
            }

            Algorithm.GetUpdatedCard(currentCard, score);
            var drillService = new DrillService();
            drillService.UpdateCard(currentCard);
            Session[CurrentCardKey] = null;
        }
    }
}
