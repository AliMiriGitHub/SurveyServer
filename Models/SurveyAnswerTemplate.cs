using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    [Table("SurveyAnswerTemplates")]
    public class SurveyAnswerTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Survey Survey { get; set; }
    }
}