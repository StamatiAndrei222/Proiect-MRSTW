using EnglishCourses.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCourses.BusinessLogic.Interface
{
    public interface IService
    {
        QuizResponse SuggestCourse(double scorePercentage);
        SearchResponse SearchCourse(string query);
    }
}
