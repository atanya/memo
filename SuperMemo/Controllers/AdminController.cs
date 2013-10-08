using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperMemo.BL;

namespace SuperMemo.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            var cardService = new CardService();

            return View("Dump", cardService.List());
        }

    }
}
