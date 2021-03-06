﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class MyUser : IdentityUser
    {
        public virtual ICollection<Survey> CreatedBy { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}