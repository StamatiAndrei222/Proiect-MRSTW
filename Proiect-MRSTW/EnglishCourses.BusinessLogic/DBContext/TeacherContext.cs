using EnglishCourses.Domain.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.DBContext
{
    public class TeacherContext : DbContext
    {
        public TeacherContext() : base("name=English")
        {
        }

        public virtual DbSet<TeacherDbTable> Teachers { get; set; }
    }
}