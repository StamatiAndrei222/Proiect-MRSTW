using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Web.Models.Teacher
{
    public class RegisterTeacher
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}