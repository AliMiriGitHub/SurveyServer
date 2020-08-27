using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Models.Auth;

namespace SurveySystem.Data
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
                
        }
    }
}