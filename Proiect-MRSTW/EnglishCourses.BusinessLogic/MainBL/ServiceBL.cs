using EnglishCourses.BusinessLogic.Core;
using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic.MainBL
{
    public class ServiceBL : ServiceAPI, IService
    {
        public QuizResponse SuggestCourse(double scorePercentage)
        {
            return SuggestCourseAction(scorePercentage);
        }

        public SearchResponse SearchCourse(string query)
        {
            return SearchCourseAction(query);
        }
    }
}