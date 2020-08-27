using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    [Table("SurveyGroups")]
    public class SurveyGroup
    {
        public Guid SurveyId { get; set; }

        public Guid GroupId { get; set; }

        public Survey Survey { get; set; }

        public Group Group { get; set; }
    }
}