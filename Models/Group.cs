using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SurveySystem.Data;

namespace SurveySystem.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}