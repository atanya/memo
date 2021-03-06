﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginInfoModel loginInfo, string returnUrl)
        {
            var user = Authorize(loginInfo);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToDefaultAction();
                }
            }
            else
            {
                loginInfo.ErrorMessage = "User not found.";
                return View(loginInfo);
            }
            return Json("");
        }

        private RedirectToRouteResult RedirectToDefaultAction()
        {
            return RedirectToAction("Index", "Drill");
        }

        private User Authorize(LoginInfoModel loginInfo)
        {
            var user = _userService.FindByNameAndPassword(loginInfo.UserName, loginInfo.Password);
            return user;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(LoginInfoModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.FindByName(loginInfo.UserName);
                if (user != null)
                {
                    loginInfo.ErrorMessage = "User already exists.";
                    return View(loginInfo);
                }
                _userService.Create(loginInfo.UserName, loginInfo.Password);
                user = _userService.FindByNameAndPassword(loginInfo.UserName, loginInfo.Password);
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToDefaultAction();
            }
            return View(loginInfo);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login");
        }

    }
}
