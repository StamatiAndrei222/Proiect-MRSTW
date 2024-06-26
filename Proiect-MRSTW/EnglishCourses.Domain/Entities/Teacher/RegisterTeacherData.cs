using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Domain.Entities.Teacher
{
    public class RegisterTeacherData
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}