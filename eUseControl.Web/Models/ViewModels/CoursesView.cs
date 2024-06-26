using EnglishCourses.Web.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModels
{
    public class CoursesView
    {
        public NavbarView NavbarView { get; set; } = new NavbarView();
        public List<CourseBrief> Courses { get; set; } = new List<CourseBrief>();
    }
}