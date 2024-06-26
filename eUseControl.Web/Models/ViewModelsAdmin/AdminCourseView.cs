using EnglishCourses.Web.Models.Course;
using EnglishCourses.Web.Models.CourseChapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModelsAdmin
{
    public class AdminCourseView
    {
        public CourseComplete Course { get; set; }
        public List<ChapterBrief> Chapters { get; set; }

    }
}