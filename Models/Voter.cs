using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    [Table("Voters")]
    public class Voter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}