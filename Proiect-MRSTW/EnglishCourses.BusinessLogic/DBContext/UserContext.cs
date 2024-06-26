using EnglishCourses.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.DBContext
{
    class UserContext : DbContext
    {
        public UserContext() : base("name=English") { }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}