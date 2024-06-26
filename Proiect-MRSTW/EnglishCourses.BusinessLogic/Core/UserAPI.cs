using AutoMapper;
using EnglishCourses.BusinessLogic.DBContext;
using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Entities.User;
using EnglishCourses.Domain.Enums;
using EnglishCourses.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.Core
{
    public class UserAPI
    {
        public Response UserLoginAction(ULoginData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Login Data Does Not Exist" };

            var pass = LoginHelper.HashGen(data.Password);
            using (var userContext = new UserContext())
            {
                var result = userContext.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                if (result == null)
                {
                    return new Response { Status = false };
                }
            }
            return new Response { Status = true };
        }

        internal Response UserLogoutAction(UserMinimal profile)
        {
            if (profile == null) return new Response { Status = false, ActionStatusMsg = "User Does Not Exist" };
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == profile.Id);
                if (user == null) return new Response { Status = false, ActionStatusMsg = "User Not Found" };
            }
            using (var db = new SessionContext())
            {
                var userSession = db.Sessions.FirstOrDefault(s => s.Username == profile.Username);
                db.Sessions.Remove(userSession);
                db.SaveChanges();
            }
            return new Response { Status = true };
        }

        public Response UserRegisterAction(URegisterData data)
        {
            UDbTable existingUser;

            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                using (var db = new UserContext())
                {
                    existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);

                    if (existingUser != null)
                    {
                        return new Response { Status = false, ActionStatusMsg = "User With Email Already Exists" };
                    }
                }
                var pass = LoginHelper.HashGen(data.Password);

                var newUser = new UDbTable
                {
                    Email = data.Email,
                    Username = data.Username,
                    Password = pass,
                    LasIp = data.LoginIp,
                    LastLogin = data.LoginDateTime,
                    level = (UserRole)0,
                };

                using (var todo = new UserContext())
                {
                    todo.Users.Add(newUser);
                    todo.SaveChanges();
                }
                return new Response { Status = true, ActionStatusMsg = "Registered New User" };


            }
            else
                return new Response { Status = false, ActionStatusMsg = "Email Invalid" };
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            int sessionTime = 60;
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(sessionTime);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(sessionTime)
                    });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}