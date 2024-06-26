using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.Quiz
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public int Answer { get; set; }
    }
}