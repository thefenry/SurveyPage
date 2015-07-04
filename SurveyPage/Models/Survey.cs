using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class Survey
    {
        public int Id { get; set; }
        //public int Expertise { get; set; }
        //public int Professionalism { get; set; }
        //public int Accountability { get; set; }
        //public virtual ApplicationUser User { get; set; }
        public string SurveyName { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}