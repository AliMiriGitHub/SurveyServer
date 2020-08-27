using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Models
{
    public class SurveyReport
    {
        public string Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<SurveyAnswerTemplate> Templates { get; set; }
        public Voter Voter { get; set; }
        public SurveyReport()
        {
            Answers = new Collection<Answer>();
            Templates = new Collection<SurveyAnswerTemplate>();
        }
    }
}