using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.Interface
{
    public interface ISession
    {
        Response UserLogin(ULoginData data);
        Response UserLogout(UserMinimal profile);
        Response UserRegistration(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }
}