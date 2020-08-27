using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Models.Views
{
    public class SurveyReportView
    {
        public string Question { get; set; }
        public ICollection<TemplateView> Templates { get; set; }
        public ICollection<ParticipantView> Participants { get; set; }
        public SurveyReportView()
        {
            Templates = new Collection<TemplateView>();
            Participants = new Collection<ParticipantView>();
        }

    }
}