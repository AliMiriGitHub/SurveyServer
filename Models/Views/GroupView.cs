using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models.Views
{
    public class GroupView
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}