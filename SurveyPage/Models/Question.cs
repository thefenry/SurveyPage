using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string SurveyQuestion { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }

        public virtual List<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}