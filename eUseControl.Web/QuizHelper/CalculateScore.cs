using EnglishCourses.Web.Models.Quiz;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace EnglishCourses.Web.QuizHelper
{
    public class CalculateScore
    {
        public double GetScore(QuizModel answers)
        {
            string jsonFilePath = HttpContext.Current.Server.MapPath("~/App_Data/quiz.json");
            string jsonText = File.ReadAllText(jsonFilePath);
            var quizData = JsonConvert.DeserializeObject<QuizModel>(jsonText);

            int totalQuestions = quizData.Questions[0].Count;
            int correctAnswers = 0;

            for (int i = 0; i < totalQuestions; i++)
            {
                int selectedAnswerIndex = answers.Questions[0][i].Answer;
                if (selectedAnswerIndex == quizData.Questions[0][i].Answer)
                {
                    correctAnswers++;
                }
            }
            double score = (double)correctAnswers / totalQuestions * 100;

            return score;
        }
    }
}