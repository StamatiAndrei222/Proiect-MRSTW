using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Entities.CourseChapter;
using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.Interface
{
    public interface IAdministration
    {
        Response AddTeacher(RegisterTeacherData data);
        Response EditTeacher(EditTeacherData data);
        Response DeleteTeacher(int id);
        Response AddCourse(RegisterCourseData data);
        Response EditCourse(EditCourseData data);
        Response DeleteCourse(int id);
        Response AddChapter(int courseId, RegisterChapterData data);
        Response EditChapter(int courseId, EditChapterData data);
        Response DeleteChapter(int chapterId, int courseId);
    }
}