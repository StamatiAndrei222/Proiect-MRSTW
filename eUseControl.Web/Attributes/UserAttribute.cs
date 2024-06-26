using EnglishCourses.BusinessLogic;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Enums;
using EnglishCourses.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnglishCourses.Web.Attributes
{
    public class UserAttribute : ActionFilterAttribute
    {
        private readonly ISession _sessionBusinessLogic;
        public UserAttribute()
        {
            var bl = new BussinesLogical();
            _sessionBusinessLogic = bl.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);
                if (profile != null && profile.Level == UserRole.User)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "Index" }));
                }
            }
        }
    }
}