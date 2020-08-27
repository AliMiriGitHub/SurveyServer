using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models.Views
{
    public class SurveyView
    {
        public string Id { get; set; }
        [Required]
        public string Question { get; set; }
        public string Description { get; set; }
        [Required]
        public TypeView Type { get; set; }
        public ICollection<TemplateView> Templates { get; set; }
        public ICollection<GroupView> Groups { get; set; }
        public SurveyView()
        {
            Templates = new Collection<TemplateView>();
            Groups = new Collection<GroupView>();
        }
    }
}