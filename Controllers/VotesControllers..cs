using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Data;
using SurveySystem.Models;
using SurveySystem.Models.Views;

namespace SurveySystem.Controllers
{
    [Route("/api/votes")]
    public class VotesControllers : Controller
    {
        private readonly SurveyContext context;
        // private readonly UserManager<ApplicationUser> userManger;
        private readonly IMapper mapper;

        public VotesControllers(SurveyContext context, IMapper mapper)
        {
            this.context = context;
            // this.userManger = userManger;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateVote([FromBody] VoteView voteView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vote = mapper.Map<VoteView, Vote>(voteView);
            var clientId = HttpContext.User.Claims.First().Value;
            vote.ClientId = new Guid(clientId);
            await context.Votes.AddAsync(vote);
            await context.SaveChangesAsync();
            var result = mapper.Map<Vote, VoteView>(vote);
            return Ok(result);
        }
    }
}