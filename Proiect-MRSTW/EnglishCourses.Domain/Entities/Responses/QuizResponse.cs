using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Domain.Entities.Responses
{
    public class QuizResponse
    {
        public string ActionStatusMsg { get; set; }
        public bool Status { get; set; }
        public int CourseId { get; set; }
    }
}