using System.Web.Http;
using DTO;
using SuperMemo.BL;
using SuperMemo.DomainModel;
using SuperMemo.Models;
using SuperMemo.SM2.Implementation;

namespace SuperMemo.Controllers
{
    [Authorize]
    public class TrainingController : ApiController
    {
        // GET api/training - get next card for training
        public ResponseObject Get()
        {
            var currentUser = new UserService().FindByName(User.Identity.Name);
            var cardService = new CardService();
            var total = cardService.Count(currentUser.Id);

            var drillService = new DrillService();
            var cards = drillService.GetCardsForDrill(currentUser.Id);

            var currentCard = new Card();
            if (cards.Count > 0)
            {
                currentCard = cards[0];
            }
            
            return ResponseObject.Success(new TrainingDto(currentCard, total, cards.Count));
        }

        // PUT api/training/5 - update card
        public void Put(string id, [FromBody]byte answer)
        {
            var currentUser = new UserService().FindByName(User.Identity.Name);
            var cardFromService = new CardService().GetCardByID(id);
            if (cardFromService == null)
            {
                return;
            }

            var score = Score.None;
            if (answer > 2 && answer < 6)
            {
                score = (Score)answer;
            }

            Algorithm.GetUpdatedCard(cardFromService, score);
            var drillService = new DrillService();
            drillService.UpdateCard(cardFromService, currentUser.Id);
        }

    }
}
