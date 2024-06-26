using EnglishCourses.Web.Models.CourseChapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModels
{
    public class CourseContentView
    {
        public NavbarView NavbarView { get; set; } = new NavbarView();
        public int CourseId { get; set; }
        public List<ChapterComplete> Chapters { get; set; } = new List<ChapterComplete>();
    }
}