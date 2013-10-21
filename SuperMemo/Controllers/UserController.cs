using System.Web.Mvc;

namespace SuperMemo.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Authorize]
        public new ActionResult Profile()
        {
            return View();
        }
    }
}
