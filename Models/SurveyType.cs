using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    [Table("SurveyTypes")]
    public class SurveyType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}