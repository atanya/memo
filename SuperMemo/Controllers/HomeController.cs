using System.Web.Mvc;
using SuperMemo.ActionFilters;

namespace SuperMemo.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
