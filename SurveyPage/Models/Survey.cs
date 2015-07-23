using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string SurveyName { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public virtual List<Question> Questions { get; set; }

        public Survey()
        {
            Questions = new List<Question>();
        }
    }
}