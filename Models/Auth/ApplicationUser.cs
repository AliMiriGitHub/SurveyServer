using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace SurveySystem.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser Parent { get; set; }
    }
}