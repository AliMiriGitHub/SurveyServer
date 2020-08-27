using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Voter Voter { get; set; }
        public ICollection<Question> Questions { get; set; }
        public Guid ClientId { get; set; }
        public Vote()
        {
            Questions = new Collection<Question>();
        }

    }
}