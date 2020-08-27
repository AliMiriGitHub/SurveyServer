using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Models
{
    public class QuestionView
    {
        public string SurveyId { get; set; }
        public ICollection<string> Answers { get; set; }
        public QuestionView()
        {
            Answers = new Collection<string>();
        }
    }
}