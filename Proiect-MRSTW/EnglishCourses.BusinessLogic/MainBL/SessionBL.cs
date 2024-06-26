using EnglishCourses.BusinessLogic.Core;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.MainBL
{
    public class SessionBL : UserAPI, ISession
    {
        public Response UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public Response UserLogout(UserMinimal profile)
        {
            return UserLogoutAction(profile);
        }

        public Response UserRegistration(URegisterData data)
        {
            return UserRegisterAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

    }
}