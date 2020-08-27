using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Models;

namespace SurveySystem.Data
{
    public class SurveyContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SurveyType> SurveyTypes { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SurveyGroup>().HasKey(sg =>
                new { sg.SurveyId, sg.GroupId }
            );

            modelBuilder.Entity<Survey>()
            .HasMany<SurveyAnswerTemplate>(s => s.Templates)
            .WithOne(t => t.Survey)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}