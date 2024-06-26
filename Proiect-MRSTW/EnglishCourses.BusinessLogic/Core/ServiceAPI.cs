using EnglishCourses.BusinessLogic.DBContext;
using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Entities.Responses;
using EnglishCourses.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.Core
{
    public class ServiceAPI
    {



        public QuizResponse SuggestCourseAction(double scorePercentage)
        {
            if (scorePercentage < 0) return new QuizResponse { Status = false, ActionStatusMsg = "Invalid ScorePercentage" };
            var course = new CourseDbTable();
            using (var db = new CourseContext())
            {
                if (scorePercentage < 30)
                {
                    course = db.Courses.FirstOrDefault(c => c.Category == CourseCategory.Beginner);
                }
                else if (scorePercentage < 70 && scorePercentage >= 30)
                {
                    course = db.Courses.FirstOrDefault(c => c.Category == CourseCategory.Intermediate);
                }
                else
                {
                    course = db.Courses.FirstOrDefault(c => c.Category == CourseCategory.Advanced);
                }
            }
            if (course == null) return new QuizResponse { Status = false, ActionStatusMsg = "No Course Was Found" };

            return new QuizResponse { Status = true, CourseId = course.Id };
        }



        public SearchResponse SearchCourseAction(string query)
        {
            using (var db = new CourseContext())
            {
                IQueryable<CourseDbTable> coursesQuery = db.Courses;

                if (query != "")
                {
                    var searchTerms = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    coursesQuery = coursesQuery.Where(c =>
                        searchTerms.Any(term =>
                            c.Title.Contains(term) ||
                            c.Description.Contains(term) ||
                            c.Category.ToString().Contains(term)
                        )
                    ).OrderByDescending(c =>
                        searchTerms.Sum(term =>
                            (c.Category.ToString().Contains(term) ? 3 :
                            (c.Title.Contains(term) ? 2 :
                            (c.Description.Contains(term) ? 1 : 0)))
                        )
                    );
                }
                else
                {
                    var list = db.Courses.OrderByDescending(c => c.Id).ToList();
                    return new SearchResponse { Status = true, Courses = list };
                }

                var courses = coursesQuery.ToList();
                return new SearchResponse { Status = true, Courses = courses };
            }
        }


    }
}