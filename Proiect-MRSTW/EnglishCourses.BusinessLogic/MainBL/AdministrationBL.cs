using EnglishCourses.BusinessLogic.Core;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Entities.CourseChapter;
using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.MainBL
{
    public class AdministrationBL : AdminAPI, IAdministration
    {
        public Response AddTeacher(RegisterTeacherData data)
        {
            return AddTeacherAction(data);
        }

        public Response EditTeacher(EditTeacherData data)
        {
            return EditTeacherAction(data);
        }

        public Response DeleteTeacher(int id)
        {
            return DeleteTeacherAction(id);
        }

        public Response AddCourse(RegisterCourseData data)
        {
            return AddCourseAction(data);
        }

        public Response EditCourse(EditCourseData data)
        {
            return EditCourseAction(data);
        }

        public Response DeleteCourse(int id)
        {
            return DeleteCourseAction(id);
        }

        public Response AddChapter(int courseId, RegisterChapterData data)
        {
            return AddChapterAction(courseId, data);
        }

        public Response EditChapter(int courseId, EditChapterData data)
        {
            return EditChapterAction(courseId, data);
        }

        public Response DeleteChapter(int chapterId, int courseId)
        {
            return DeleteChapterAction(chapterId, courseId);
        }
    }
}