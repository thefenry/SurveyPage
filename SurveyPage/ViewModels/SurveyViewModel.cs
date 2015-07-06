using SurveyPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
            this.SurveyQuestions = new List<Question>();
        }

        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public IList<Question> SurveyQuestions { get; set; }
    }
}