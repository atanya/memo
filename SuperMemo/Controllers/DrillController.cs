using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
namespace SuperMemo.Controllers
{
    public class DrillController : Controller
    {
        //
        // GET: /Drill/

        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
