using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    
    [Table("Answers")]
    public class Answer
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}