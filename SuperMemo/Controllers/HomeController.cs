using System.Web.Mvc;

namespace SuperMemo.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
