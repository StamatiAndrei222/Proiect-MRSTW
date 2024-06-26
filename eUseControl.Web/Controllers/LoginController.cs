using EnglishCourses.BusinessLogic;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Entities.User;
using EnglishCourses.Web.Extension;
using EnglishCourses.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EnglishCourses.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BussinesLogical();
            _session = bl.GetSessionBL();

        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };
                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.ActionStatusMsg);
                    return View();
                }
            }
            return View();

        }
        public ActionResult Logout()
        {
            var profile = System.Web.HttpContext.Current.GetMySessionObject();
            var userLogout = _session.UserLogout(profile);
            if (userLogout.Status == false)
            {
                ModelState.AddModelError("", userLogout.ActionStatusMsg);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}