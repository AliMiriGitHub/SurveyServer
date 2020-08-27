using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Data;
using SurveySystem.Models;
using SurveySystem.Models.Views;

namespace SurveySystem.Controllers
{
    [Route("/api/Groups")]
    public class GroupsControllers : Controller
    {
        private readonly SurveyContext context;
        private readonly IMapper mapper;

        public GroupsControllers(SurveyContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupView groupView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var group = mapper.Map<GroupView, Group>(groupView);
            var userId = HttpContext.User.Claims.First().Value;
            group.UserId = userId;
            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
            var result = mapper.Map<Group, GroupView>(group);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupView groupView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var group = await context.Groups.FindAsync(new Guid(groupView.Id));
            if (group == null)
                return NotFound();
            mapper.Map<GroupView, Group>(groupView, group);
            context.Groups.Update(group);
            await context.SaveChangesAsync();
            var result = mapper.Map<Group, GroupView>(group);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            var group = await context.Groups.FindAsync(new Guid(id));
            if (group == null)
                return NotFound();
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GroupList")]
        public async Task<IEnumerable<GroupView>> GetGroups()
        {
            var userId = HttpContext.User.Claims.First().Value;
            var groups = await context.Groups.Where(g => g.UserId == userId).ToListAsync();
            return mapper.Map<IEnumerable<Group>, IEnumerable<GroupView>>(groups);
        }
    }
}