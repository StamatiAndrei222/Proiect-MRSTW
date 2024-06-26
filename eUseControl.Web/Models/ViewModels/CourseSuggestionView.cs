using EnglishCourses.Web.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModels
{
    public class CourseSuggestionView
    {
        public NavbarView NavbarView { get; set; } = new NavbarView();
        public CourseSuggestionModel Course { get; set; }
    }
}