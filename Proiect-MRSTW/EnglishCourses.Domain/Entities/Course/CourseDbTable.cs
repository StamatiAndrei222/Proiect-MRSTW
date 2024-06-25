using EnglishCourses.Domain.Entities.CourseChapter;
using EnglishCourses.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishCourses.Domain.Entities.Course
{
    public class CourseDbTable
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseCategory Category { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime DateCreated { get; set; }
        public int StudentsCount { get; set; } = 0;
        public byte[] DisplayImage { get; set; }
        public virtual ICollection<ChapterDbTable> Chapters { get; set; } = new List<ChapterDbTable>();


    }
}