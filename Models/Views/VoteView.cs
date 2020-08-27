using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Models.Views
{
    public class VoteView
    {
        public VoterView Voter { get; set; }
        public ICollection<QuestionView> Questions { get; set; }
        public VoteView()
        {
            Questions = new Collection<QuestionView>();
        }
    }
}