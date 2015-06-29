﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyPage.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string SurveyQuestion { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}