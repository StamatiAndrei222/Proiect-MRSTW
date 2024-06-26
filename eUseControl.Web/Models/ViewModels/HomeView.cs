using EnglishCourses.Web.Models.Course;
using EnglishCourses.Web.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModels
{
    public class HomeView
    {
        public NavbarView NavbarView { get; set; } = new NavbarView();
        public List<CourseBrief> Courses { get; set; } = new List<CourseBrief>();
        public List<TeacherBrief> Teachers { get; set; } = new List<TeacherBrief>();
    }
}