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
    [Authorize]
    public class DrillController : Controller
    {
        private const string CurrentCardKey = "CurrentCard";
        //
        // GET: /Drill/

        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
