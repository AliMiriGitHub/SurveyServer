using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Models.Views
{
    public class ParticipantView
    {
        public ICollection<AnswerView> Answers { get; set; }
        public VoterView Voter { get; set; }
    }
}