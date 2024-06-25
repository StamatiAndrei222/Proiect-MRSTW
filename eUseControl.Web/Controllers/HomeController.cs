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
using EnglishCourses.Web.Models.Quiz;
using EnglishCourses.Web.Models.Teacher;
using EnglishCourses.Web.Models.ViewModels;
using EnglishCourses.Web.QuizHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace EnglishCourses.Web.Controllers
{
    [User]
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IService _service;
        public HomeController()
        {
            var bl = new BussinesLogical();
            _session = bl.GetSessionBL();
            _service = bl.GetServiceBL();

        }

        public ActionResult Index()
        {
            HomeView homeView = new HomeView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) homeView.NavbarView.Authenticated = true;

            using (var db = new TeacherContext())
            {
                var teachersData = db.Teachers.OrderByDescending(x => x.Id).Take(5).ToList();
                var teachersList = Mapper.Map<List<TeacherDbTable>, List<TeacherBrief>>(teachersData);
                homeView.Teachers = teachersList;
            }

            using (var db = new CourseContext())
            {
                var coursesData = db.Courses.OrderByDescending(c => c.Id).Take(5).ToList();
                var coursesList = Mapper.Map<List<CourseDbTable>, List<CourseBrief>>(coursesData);
                homeView.Courses = coursesList;
            }
            return View(homeView);
        }

        public ActionResult TakeQuiz()
        {
            var quizView = new QuizView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) quizView.NavbarView.Authenticated = true;

            string jsonFilePath = Server.MapPath("~/App_Data/quiz.json");
            string jsonText = System.IO.File.ReadAllText(jsonFilePath);
            var quizData = JsonConvert.DeserializeObject<QuizModel>(jsonText);
            quizView.Quiz = quizData;
            return View(quizView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeQuiz(QuizModel answers)
        {
            if (ModelState.IsValid)
            {
                var helper = new CalculateScore();
                var getSuggestion = _service.SuggestCourse(helper.GetScore(answers));
                if (getSuggestion.Status)
                {

                    return RedirectToAction("CourseSuggestion", "Home", new { id = getSuggestion.CourseId });
                }
                else
                {
                    ModelState.AddModelError("", getSuggestion.ActionStatusMsg);
                    if (getSuggestion.ActionStatusMsg == "No Course Was Found") return RedirectToAction("CourseSuggestion", "Home");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CourseSuggestion(int? id)
        {
            var courseSuggestionView = new CourseSuggestionView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) courseSuggestionView.NavbarView.Authenticated = true;
            using (var db = new CourseContext())
            {
                var course = db.Courses.FirstOrDefault(c => c.Id == id);
                courseSuggestionView.Course = Mapper.Map<CourseSuggestionModel>(course);
            }
            return View(courseSuggestionView);
        }

        public ActionResult Courses()
        {
            var coursesView = new CoursesView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) coursesView.NavbarView.Authenticated = true;
            using (var db = new CourseContext())
            {
                var coursesData = db.Courses.OrderByDescending(c => c.Id).ToList();
                coursesView.Courses = Mapper.Map<List<CourseDbTable>, List<CourseBrief>>(coursesData);
            }
            return View(coursesView);
        }


        public ActionResult CourseDetails(int id)
        {
            var courseDetailsView = new CourseDetailsView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) courseDetailsView.NavbarView.Authenticated = true;
            using (var db = new CourseContext())
            {
                var courseData = db.Courses.FirstOrDefault(c => c.Id == id);
                if (courseData == null) RedirectToAction("Courses", "Home");
                courseDetailsView.Course = Mapper.Map<CourseDetails>(courseData);
            }
            return View(courseDetailsView);
        }

        public ActionResult CourseContent(int id)
        {
            var courseContent = new CourseContentView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) courseContent.NavbarView.Authenticated = true;
            using (var db = new CourseContext())
            {
                var course = db.Courses.Include("Chapters").FirstOrDefault(c => c.Id == id);
                courseContent.Chapters = Mapper.Map<List<ChapterDbTable>, List<ChapterComplete>>(course.Chapters.ToList());
                courseContent.CourseId = course.Id;
            }
            return View(courseContent);
        }

        [HttpPost]
        public ActionResult SearchCourse(string query)
        {
            var searchCourse = _service.SearchCourse(query);
            var coursesView = new CoursesView();
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            if (currentUser != null) coursesView.NavbarView.Authenticated = true;
            if (searchCourse.Status)
            {
                coursesView.Courses = Mapper.Map<List<CourseDbTable>, List<CourseBrief>>(searchCourse.Courses);
                return View("Courses", coursesView);
            }
            coursesView.Courses = Mapper.Map<List<CourseDbTable>, List<CourseBrief>>(searchCourse.Courses);
            return View("Courses", coursesView);
        }

    }
}