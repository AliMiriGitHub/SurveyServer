using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{

    [Table("Questions")]
    public class Question
    {
        public Guid Id { get; set; }
        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Question()
        {
            Answers = new Collection<Answer>();
        }
    }   
}