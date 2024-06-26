using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.Domain.Entities.Teacher
{
    public class EditTeacherData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}