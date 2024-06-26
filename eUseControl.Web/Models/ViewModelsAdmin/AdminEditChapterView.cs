using EnglishCourses.Web.Models.CourseChapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModelsAdmin
{
    public class AdminEditChapterView
    {
        public int chapterId { get; set; }
        public EditChapter Chapter { get; set; }
    }
}