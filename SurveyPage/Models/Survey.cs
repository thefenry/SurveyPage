using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [Display(Name="Survey Name")]
        public string SurveyName { get; set; }
        
        public string CreatedByID { get; set; }
        [Display(Name = "Created By")]
        public virtual ApplicationUser CreatedBy { get; set; }
        
        public virtual List<Question> Questions { get; set; }

        public Survey()
        {
            Questions = new List<Question>();
        }
    }
}