using EnglishCourses.BusinessLogic;
using EnglishCourses.BusinessLogic.Core;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Entities.User;
using EnglishCourses.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishCourses.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserAPI _userAPI;

        private readonly ISession _session;
        public RegistrationController()
        {
            var bl = new BussinesLogical();
            _session = bl.GetSessionBL();
            _userAPI = new UserAPI();

        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistration registration)
        {


            if (ModelState.IsValid)
            {
                var registerData = new URegisterData
                {
                    Username = registration.Username,
                    Password = registration.Password,
                    Email = registration.Email,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var userRegister = _session.UserRegistration(registerData);

                if (userRegister.Status)
                {
                    HttpCookie cookie = _session.GenCookie(registration.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userRegister.ActionStatusMsg);
                    return View();
                }
            }
            return View();
        }


    }
}