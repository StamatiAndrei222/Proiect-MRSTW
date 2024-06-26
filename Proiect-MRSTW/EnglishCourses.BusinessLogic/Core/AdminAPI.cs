using EnglishCourses.BusinessLogic.DBContext;
using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Entities.CourseChapter;
using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Entities.Teacher;
using EnglishCourses.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace EnglishCourses.BusinessLogic.Core
{
    public class AdminAPI
    {

        public Response AddTeacherAction(RegisterTeacherData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Teacher Register Data Does Not Exist" };
            var profilePicture = FileHelper.ConvertToByteArray(data.ProfilePicture);
            var newTeacher = new TeacherDbTable
            {
                Name = data.Name,
                Biography = data.Biography,
                JoinDate = DateTime.Now,
                TotalStudents = 0,
                ProfilePicture = profilePicture,
            };

            using (var db = new TeacherContext())
            {
                db.Teachers.Add(newTeacher);
                db.SaveChanges();
            }
            return new Response { Status = true };
        }



        public Response EditTeacherAction(EditTeacherData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Teacher Edit Data Does Not Exist" };

            using (var db = new TeacherContext())
            {
                var currentTeacher = db.Teachers.FirstOrDefault(t => t.Id == data.Id);
                if (currentTeacher == null) return new Response { Status = false, ActionStatusMsg = "Teacher Not Found" };
                if (data.ProfilePicture != null)
                {
                    var newPicture = FileHelper.ConvertToByteArray(data.ProfilePicture);
                    currentTeacher.ProfilePicture = newPicture;
                }
                currentTeacher.Name = data.Name;
                currentTeacher.Biography = data.Biography;
                db.Entry(currentTeacher).State = EntityState.Modified;
                db.SaveChanges();
            }
            return new Response { Status = true };
        }

        public Response DeleteTeacherAction(int id)
        {
            using (var db = new TeacherContext())
            {
                var currentTeacher = db.Teachers.FirstOrDefault(t => t.Id == id);
                if (currentTeacher == null) return new Response { Status = false, ActionStatusMsg = "Teacher to Delete Not Found" };
                db.Teachers.Remove(currentTeacher);
                db.SaveChanges();
            }
            return new Response { Status = true };
        }

        public Response AddCourseAction(RegisterCourseData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Register Course Information Does Not Exist" };

            TeacherDbTable teacher;
            using (var db = new TeacherContext())
            {
                teacher = db.Teachers.FirstOrDefault(t => t.Name == data.TeacherName);
                if (teacher == null) return new Response { Status = false, ActionStatusMsg = "Course Teacher Was Not Found" };
            }
            var displayImage = FileHelper.ConvertToByteArray(data.DisplayImage);
            using (var db = new CourseContext())
            {

                var newCourse = new CourseDbTable
                {
                    Title = data.Title,
                    Description = data.Description,
                    Category = data.Category,
                    TeacherId = teacher.Id,
                    TeacherName = data.TeacherName,
                    DateCreated = DateTime.Now,
                    DisplayImage = displayImage,
                };

                db.Courses.Add(newCourse);
                db.SaveChanges();

            }
            return new Response { Status = true };
        }

        public Response EditCourseAction(EditCourseData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Course Edit Data Does Not Exist" };
            TeacherDbTable teacher;
            using (var db = new TeacherContext())
            {
                teacher = db.Teachers.FirstOrDefault(t => t.Name == data.TeacherName);
                if (teacher == null) return new Response { Status = false, ActionStatusMsg = "Course Teacher Was Not Found" };
            }
            using (var db = new CourseContext())
            {
                var course = db.Courses.FirstOrDefault(c => c.Id == data.Id);
                if (course == null) return new Response { Status = false, ActionStatusMsg = "Course Could Not be Found" };
                if (data.DisplayImage != null)
                {
                    course.DisplayImage = FileHelper.ConvertToByteArray(data.DisplayImage);
                }
                course.Title = data.Title;
                course.Description = data.Description;
                course.Category = data.Category;
                course.TeacherId = teacher.Id;
                course.TeacherName = data.TeacherName;

                db.SaveChanges();
            }

            return new Response { Status = true };
        }

        public Response DeleteCourseAction(int id)
        {
            using (var db = new CourseContext())
            {
                var course = db.Courses.FirstOrDefault(c => c.Id == id);
                if (course == null) return new Response { Status = false, ActionStatusMsg = "Course Was Not Found" };
                db.Courses.Remove(course);
                db.SaveChanges();
            }
            return new Response() { Status = true };
        }

        public Response AddChapterAction(int courseId, RegisterChapterData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Chapter Data Does Not Exist" };
            using (var db = new CourseContext())
            {
                var course = db.Courses.Include("Chapters").FirstOrDefault(c => c.Id == courseId);
                if (course == null) return new Response { Status = false, ActionStatusMsg = "Course Was Not Found" };
                var newChapter = new ChapterDbTable
                {
                    CourseId = courseId,
                    Title = data.Title,
                    Description = data.Description,
                    Content = data.Content,
                };
                course.Chapters.Add(newChapter);
                db.SaveChanges();
            }
            return new Response() { Status = true };
        }

        public Response EditChapterAction(int courseId, EditChapterData data)
        {
            if (data == null) return new Response { Status = false, ActionStatusMsg = "Chapter Edit Data Does Not Exist" };

            using (var db = new CourseContext())
            {
                var course = db.Courses.Include("Chapters").FirstOrDefault(c => c.Id == courseId);
                if (course == null) return new Response { Status = false, ActionStatusMsg = "Course Was Not Found" };
                var chapter = course.Chapters.FirstOrDefault(c => c.Id == data.Id);
                if (chapter == null) return new Response { Status = false, ActionStatusMsg = "Chapter Was Not Found" };

                chapter.Title = data.Title;
                chapter.Description = data.Description;
                chapter.Content = data.Content;
                db.SaveChanges();
            }

            return new Response() { Status = true };
        }

        public Response DeleteChapterAction(int chapterId, int courseId)
        {
            using (var db = new CourseContext())
            {
                var course = db.Courses.Include("Chapters").FirstOrDefault(c => c.Id == courseId);
                if (course == null) return new Response { Status = false, ActionStatusMsg = "Course Was Not Found" };
                var chapter = course.Chapters.FirstOrDefault(c => c.Id == chapterId);
                if (chapter == null) return new Response { Status = false, ActionStatusMsg = "Chapter Was Not Found" };

                course.Chapters.Remove(chapter);
                db.Entry(chapter).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return new Response { Status = true };
        }

    }
}