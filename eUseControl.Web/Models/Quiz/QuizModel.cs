using EnglishCourses.Web.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.Quiz
{
    public class QuizModel
    {
        public List<List<QuizQuestion>> Questions { get; set; }
    }
}