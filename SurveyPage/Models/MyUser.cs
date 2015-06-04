using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class MyUser : IdentityUser
    {
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}