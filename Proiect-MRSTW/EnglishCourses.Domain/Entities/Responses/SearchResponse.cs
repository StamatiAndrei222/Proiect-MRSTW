using EnglishCourses.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Domain.Entities.Responses
{
    public class SearchResponse
    {
        public string ActionStatusMsg { get; set; }
        public bool Status { get; set; }
        public List<CourseDbTable> Courses { get; set; } = new List<CourseDbTable>();
    }
}