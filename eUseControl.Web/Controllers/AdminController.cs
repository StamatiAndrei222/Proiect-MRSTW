using AutoMapper;
using EnglishCourses.BusinessLogic;
using EnglishCourses.BusinessLogic.DBContext;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Entities.CourseChapter;
using EnglishCourses.Domain.Entities.Teacher;
using EnglishCourses.Web.Attributes;
using EnglishCourses.Web.Extension;
using EnglishCourses.Web.Models.Course;
using EnglishCourses.Web.Models.CourseChapter;
using EnglishCourses.Web.Models.Teacher;
using EnglishCourses.Web.Models.ViewModelsAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishCourses.Web.Controllers
{
    [Admin]
    public class AdminController : BaseController
    {
        private readonly ISession _session;
        private readonly IAdministration _administration;
        public AdminController()
        {
            var bl = new BussinesLogical();
            _session = bl.GetSessionBL();
            _administration = bl.GetAdministrationBL();

        }

        private AdminNavbarView GetNavbarData()
        {
            AdminNavbarView navbarView = new AdminNavbarView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) navbarView.Authenticated = true;
            return navbarView;
        }

        public ActionResult Index()
        {

            AdminHomeView homeView = new AdminHomeView();
            homeView.AdminNavbarView = GetNavbarData();
            return View(homeView);
        }

        public ActionResult Teachers()
        {
            AdminTeachersView teachersView = new AdminTeachersView();
            List<TeacherDbTable> teachersData;
            using (var db = new TeacherContext())
            {
                teachersData = db.Teachers.OrderByDescending(t => t.Id).ToList();
            }
            var teachersList = Mapper.Map<List<TeacherDbTable>, List<TeacherBrief>>(teachersData);
            teachersView.Teachers = teachersList;
            teachersView.AdminNavbarView = GetNavbarData();
            return View(teachersView);
        }

        public ActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeacher(RegisterTeacher teacher)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<RegisterTeacherData>(teacher);
                var registerTeacher = _administration.AddTeacher(data);
                if (registerTeacher.Status)
                {
                    return RedirectToAction("Teachers");
                }
                else
                {
                    ModelState.AddModelError("", registerTeacher.ActionStatusMsg);
                    return View();
                }
            }

            return View();
        }

        public ActionResult EditTeacher(int id)
        {
            TeacherDbTable data;
            AdminEditTeacherView editView = new AdminEditTeacherView();
            using (var db = new TeacherContext())
            {
                data = db.Teachers.FirstOrDefault(t => t.Id == id);
            }
            if (data == null)
            {
                ModelState.AddModelError("", "Teacher For Edit Not Found");
                return RedirectToAction("Teachers");
            }
            editView.Teacher = Mapper.Map<EditTeacher>(data);
            return View(editView.Teacher);
        }

        [HttpPost]
        public ActionResult EditTeacher(EditTeacher teacher)
        {
            var data = Mapper.Map<EditTeacherData>(teacher);
            var editTeacher = _administration.EditTeacher(data);
            if (editTeacher.Status)
            {
                return RedirectToAction("Teachers");
            }
            else
            {
                ModelState.AddModelError("", editTeacher.ActionStatusMsg);
                return RedirectToAction("EditTeacher", "Admin", new { id = teacher.Id });
            }
        }

        public ActionResult DeleteTeacher(int id)
        {
            var deleteTeacher = _administration.DeleteTeacher(id);
            if (deleteTeacher.Status == false)
            {
                ModelState.AddModelError("", deleteTeacher.ActionStatusMsg);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Teachers");
        }

        public ActionResult Courses()
        {
            AdminCoursesView coursesView = new AdminCoursesView();
            List<CourseDbTable> coursesData;
            using (var db = new CourseContext())
            {
                coursesData = db.Courses.OrderByDescending(t => t.Id).ToList();
            }
            var coursesList = Mapper.Map<List<CourseDbTable>, List<CourseBrief>>(coursesData);
            coursesView.Courses = coursesList;
            coursesView.AdminNavbarView = GetNavbarData();
            return View(coursesView);
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(RegisterCourse course)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<RegisterCourseData>(course);
                var registerCourse = _administration.AddCourse(data);
                if (registerCourse.Status)
                {
                    return RedirectToAction("Courses");
                }
                else
                {
                    ModelState.AddModelError("", registerCourse.ActionStatusMsg);
                    return View();
                }
            }
            return View();
        }

        public ActionResult CourseDetails(int? id)
        {
            if (id == null) RedirectToAction("Index", "Admin");
            AdminCourseView courseView = new AdminCourseView();
            using (var db = new CourseContext())
            {
                var data = db.Courses.FirstOrDefault(c => c.Id == id);
                courseView.Course = Mapper.Map<CourseComplete>(data);
                courseView.Chapters = Mapper.Map<List<ChapterDbTable>, List<ChapterBrief>>(data.Chapters.ToList());
            }
            return View(courseView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(EditCourse course)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<EditCourseData>(course);
                var editCourse = _administration.EditCourse(data);
                if (editCourse.Status)
                {
                    return RedirectToAction("CourseDetails", "Admin", new { id = course.Id });
                }
                else
                {
                    ModelState.AddModelError("", editCourse.ActionStatusMsg);
                    return RedirectToAction("CourseDetails", "Admin", new { id = course.Id });
                }
            }
            return RedirectToAction("CourseDetails", "Admin", new { id = course.Id });
        }


        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
            var deleteCourse = _administration.DeleteCourse(id);
            if (deleteCourse.Status)
            {
                return Json(new { success = true });

            }
            else return Json(new { success = false, message = deleteCourse.ActionStatusMsg });
        }

        public ActionResult AddChapter(int courseId)
        {
            ViewBag.courseId = courseId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChapter(RegisterChapter chapter, int courseId)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<RegisterChapterData>(chapter);
                var addChapter = _administration.AddChapter(courseId, data);
                if (addChapter.Status)
                {
                    RedirectToAction("CourseDetails", "Admin", new { id = courseId });
                }
                else
                {
                    ModelState.AddModelError("", addChapter.ActionStatusMsg);
                    return RedirectToAction("CourseDetails", "Admin", new { id = courseId });
                }
            }
            return RedirectToAction("CourseDetails", "Admin", new { id = courseId });
        }

        public ActionResult EditChapter(int chapterId, int courseId)
        {
            AdminEditChapterView chapterView = new AdminEditChapterView();
            using (var db = new CourseContext())
            {
                var course = db.Courses.Include("Chapters").FirstOrDefault(c => c.Id == courseId);
                if (course != null)
                {
                    var chapter = course.Chapters.FirstOrDefault(c => c.Id == chapterId);
                    chapterView.Chapter = Mapper.Map<EditChapter>(chapter);
                }
                chapterView.chapterId = chapterId;
                return View(chapterView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChapter(EditChapter chapter)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<EditChapterData>(chapter);
                var edidChapter = _administration.EditChapter(chapter.CourseId, data);
                if (edidChapter.Status)
                {
                    RedirectToAction("CourseDetails", "Admin", new { id = chapter.CourseId });
                }
                else
                {
                    ModelState.AddModelError("", edidChapter.ActionStatusMsg);
                    return RedirectToAction("CourseDetails", "Admin", new { id = chapter.CourseId });
                }
            }
            return RedirectToAction("CourseDetails", "Admin", new { id = chapter.CourseId });
        }

        [HttpPost]
        public ActionResult DeleteChapter(int chapterId, int courseId)
        {
            var deleteChapter = _administration.DeleteChapter(chapterId, courseId);
            if (deleteChapter.Status)
            {
                return Json(new { success = true });

            }
            else return Json(new { success = false, message = deleteChapter.ActionStatusMsg });
        }


    }
}