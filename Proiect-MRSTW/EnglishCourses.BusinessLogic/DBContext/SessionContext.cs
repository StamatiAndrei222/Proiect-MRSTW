using EnglishCourses.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace EnglishCourses.BusinessLogic.DBContext
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=English")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}