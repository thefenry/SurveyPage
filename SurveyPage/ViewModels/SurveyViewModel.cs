using SurveyPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.ViewModels
{
    public class SurveyQuestionViewModel
    {
        public SurveyQuestionViewModel()
        {
            this.SurveyQuestions = new List<Question>();
        }

        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        //public ApplicationUser User { get; set; }
        public IList<Question> SurveyQuestions { get; set; }
    }

    public class SurveyResponseViewModel
    {
        public SurveyResponseViewModel()
        {
            this.SurveyQuestions = new List<Question>();
            this.QuestionAnswers = new List<Answer>();
        }

        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public List<Question> SurveyQuestions { get; set; }
        public List<Answer> QuestionAnswers { get; set; }
    }



}