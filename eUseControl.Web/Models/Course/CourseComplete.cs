using EnglishCourses.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EnglishCourses.Web.Models.Course
{
    public class CourseComplete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseCategory Category { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime DateCreated { get; set; }
        public int StudentsCount { get; set; }
        public HttpPostedFileBase DisplayImage { get; set; }
    }
}