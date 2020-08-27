using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurveySystem.Data;

namespace SurveySystem.Migrations
{
    public static class DataSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var userContext = serviceScope.ServiceProvider.GetService<UserDbContext>();
                if (!userContext.Database.EnsureCreated())
                    userContext.Database.Migrate();
            }
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var surveyContext = serviceScope.ServiceProvider.GetService<SurveyContext>();
                if (!surveyContext.Database.EnsureCreated())
                    surveyContext.Database.Migrate();
            }
        }
    }
}