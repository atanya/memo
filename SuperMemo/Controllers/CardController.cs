using System.Collections.Generic;
using System.Web.Mvc;
using SuperMemo.BL;
using SuperMemo.DomainModel;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View("EditCard", new CardViewModel());
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View("EditCard", new CardViewModel{ID = id});
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = new CardListViewModel { Cards = new List<Card>() };
            return View("List", model);
        }


    }
}
