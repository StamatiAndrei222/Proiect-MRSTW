using EnglishCourses.Web.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModels
{
    public class QuizView
    {
        public NavbarView NavbarView { get; set; } = new NavbarView();
        public QuizModel Quiz { get; set; }
    }
}