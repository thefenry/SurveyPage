﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyPage.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionAnswer { get; set; }
        public int QuestionId { get; set; } 
        public virtual Question Question { get; set; }
        public string AnsweredById { get; set; }
        public virtual ApplicationUser AnsweredBy { get; set; }
    }
}
