using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperMemo.ActionFilters;
using SuperMemo.BL;
using SuperMemo.DomainModel;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserService _userService;

        public AuthorizationController()
        {
            _userService = new UserService();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfoModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.FindByNameAndPassword(loginInfo.UserName, loginInfo.Password);
                if (user != null)
                {
                    SetCookie(user);
                    return RedirectToAction("Index", "Home");
                }
                loginInfo.ErrorMessage = "Нет пользователя с таким именем/паролем.";
                return View(loginInfo);
            }
            return View(loginInfo);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(LoginInfoModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.FindByName(loginInfo.UserName);
                if (user != null)
                {
                    loginInfo.ErrorMessage = "Пользователь с таким именем уже существует.";
                    return View(loginInfo);
                }
                _userService.Create(loginInfo.UserName, loginInfo.Password);
                user = _userService.FindByNameAndPassword(loginInfo.UserName, loginInfo.Password);
                SetCookie(user);
                return RedirectToAction("Index", "Home");
            }
            return View(loginInfo);
        }

        [AuthorizationFilter]
        public ActionResult Logout()
        {
            Response.Cookies.Set(new HttpCookie("SuperMemoSession") {Expires = DateTime.Now.AddDays(-1)});
            return RedirectToAction("Login");
        }

        private void SetCookie(User user)
        {
            if (Response.Cookies.AllKeys.Contains("SuperMemoSession"))
            {
                Response.Cookies.Set(CreateCookie(user));
            }
            else
            {
                Response.Cookies.Add(CreateCookie(user));
            }
        }

        private static HttpCookie CreateCookie(User user)
        {
            return new HttpCookie("SuperMemoSession", user.PasswordHash)
                {
                    Expires = DateTime.Now.AddDays(7)
                };
        }
    }
}
