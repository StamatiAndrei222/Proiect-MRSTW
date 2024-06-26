using EnglishCourses.Web.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.ViewModelsAdmin
{
    public class AdminTeachersView
    {
        public AdminNavbarView AdminNavbarView { get; set; }
        public List<TeacherBrief> Teachers { get; set; }
    }
}