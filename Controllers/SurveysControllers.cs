using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Data;
using SurveySystem.Models;
using SurveySystem.Models.Auth;
using SurveySystem.Models.Views;

namespace SurveySystem.Controllers
{
    [Route("/api/surveys")]
    public class SurveysControllers : Controller
    {
        private readonly SurveyContext context;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IMapper mapper;

        public SurveysControllers(SurveyContext context, UserManager<ApplicationUser> userManger, IMapper mapper)
        {
            this.context = context;
            this.userManger = userManger;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] SurveyView surveyView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var survey = mapper.Map<SurveyView, Survey>(surveyView);
            var userId = HttpContext.User.Claims.First().Value;
            survey.UserId = new Guid(userId);
            await context.Surveys.AddAsync(survey);
            await context.SaveChangesAsync();
            var result = mapper.Map<Survey, SurveyView>(survey);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateSurvey([FromBody] SurveyView surveyView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var survey = await context.Surveys.Include(s => s.Groups).Include(s => s.Templates).SingleOrDefaultAsync(s => s.Id == new Guid(surveyView.Id));
            if (survey == null)
                return NotFound();
            mapper.Map<SurveyView, Survey>(surveyView, survey);
            context.Surveys.Update(survey);
            await context.SaveChangesAsync();
            var result = mapper.Map<Survey, SurveyView>(survey);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("Types")]
        public async Task<IEnumerable<SurveyType>> GetSurveyTypes()
        {
            var surveyTypes = await context.SurveyTypes.ToListAsync();
            return surveyTypes;
        }

        [Authorize]
        [HttpGet]
        [Route("SurveyList")]
        public async Task<IEnumerable<SurveyView>> GetSurveys()
        {
            var userId = HttpContext.User.Claims.First().Value;
            var surveys = await context.Surveys
            .Where(s => s.UserId == new Guid(userId))
            .ToListAsync();

            return mapper.Map<IEnumerable<Survey>, IEnumerable<SurveyView>>(surveys);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<SurveyView> GetSurvey(Guid id)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var survey = await context.Surveys
            .Include(s => s.Groups).ThenInclude(sg => sg.Group)
            .Include(s => s.Templates)
            .Include(s => s.Type)
            .FirstOrDefaultAsync(s => s.UserId == new Guid(userId) && s.Id == id);

            return mapper.Map<Survey, SurveyView>(survey);
        }

        [Authorize]
        [HttpGet]
        [Route("ClientSurveys")]
        public async Task<IEnumerable<SurveyView>> GetClientSurveys()
        {
            var clientId = HttpContext.User.Claims.First().Value;
            var client = await userManger.Users.Include(u => u.Parent).SingleOrDefaultAsync(u => u.Id == clientId);
            if (client != null && client.Parent != null)
            {
                 context.Surveys.Include(s => s.Templates).ToList();

                var surveys = await context.Surveys
                .Include(s => s.Templates)
                .Include(s => s.Type)
                 .Where(s => s.UserId == new Guid(client.Parent.Id))
                .ToListAsync();
                return mapper.Map<IEnumerable<Survey>, IEnumerable<SurveyView>>(surveys);
            }
            return null;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey(string id)
        {
            var survey = await context.Surveys.FindAsync(new Guid(id));
            if (survey == null)
                return NotFound();
            context.Surveys.Remove(survey);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("SurveyReports/{id}")]
        public async Task<SurveyReportView> GetSurveyReports(Guid id)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var survey = await context.Surveys.FirstOrDefaultAsync(s => s.Id == id);
            var surveyReports = await context.Votes
            .SelectMany(v => v.Questions.Where(q => q.Survey == survey && q.Survey.UserId == new Guid(userId)),
            (v, q) => new SurveyReport
            {
                Question = q.Survey.Question,
                Answers = q.Answers,
                Voter = v.Voter,
                Templates = q.Survey.Templates
            })
            .ToListAsync();

            var result = mapper.Map<IEnumerable<SurveyReport>, SurveyReportView>(surveyReports);

            return result;
        }
    }
}