using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class Survey
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public Guid TypeId { get; set; }
        public SurveyType Type { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ICollection<SurveyAnswerTemplate> Templates { get; set; }
        public ICollection<SurveyGroup> Groups { get; set; }
        public Survey()
        {
            Templates = new Collection<SurveyAnswerTemplate>();
            Groups = new Collection<SurveyGroup>();
        }
    }
}
