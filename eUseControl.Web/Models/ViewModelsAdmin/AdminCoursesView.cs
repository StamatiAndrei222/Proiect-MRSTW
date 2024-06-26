using EnglishCourses.Web.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModelsAdmin
{
    public class AdminCoursesView
    {
        public AdminNavbarView AdminNavbarView { get; set; }
        public List<CourseBrief> Courses { get; set; }
    }
}