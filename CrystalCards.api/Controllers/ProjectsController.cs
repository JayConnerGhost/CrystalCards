using System;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrystalCards.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ApplicationDbContext context, ILogger<ProjectsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("[action]/{projectId}")]
        public async Task<IActionResult> RemoveCardFromProject([FromBody] RemoveCardFromProjectRequest request,
            int projectId)
        {
            var targetProject = await _context.Projects
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Id == projectId);
            var targetCard = await _context.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (targetProject == null)
            {
                return BadRequest();
            }

            if (targetCard == null)
            {
                return BadRequest();
            }
            _context.Projects.Update(targetProject);
            targetProject.Cards.Remove(targetCard);
            await _context.SaveChangesAsync();
            return Ok(ConvertToProjectResponse(targetProject));
        }


        [HttpPost("[action]/{projectId}")]
        public async Task<IActionResult> AddCardToProject([FromBody] AddCardToProjectRequest request, int projectId)
        {
          var targetProject= await _context.Projects
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Id == projectId);
          var targetCard = await _context.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId);

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }
            
          if (targetProject == null)
          {
              return BadRequest();
          }

          if (targetCard == null)
          {
              return BadRequest();
          }

          _context.Projects.Update(targetProject);
          targetProject.Cards.Add(targetCard);
          await _context.SaveChangesAsync();

          return Ok(ConvertToProjectResponse(targetProject));
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var target =await _context.Projects
                .Include(x=>x.Cards)
                .FirstOrDefaultAsync(x => x.Id == projectId);
            if (target == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Projects.Remove(target);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
              _logger.LogError(Guid.NewGuid().ToString(),e);
            }
        
            return StatusCode(204);

        }

        [HttpPost("{username}")]
        public async Task<IActionResult> PostWithUserName([FromBody] NewProjectRequest request, string username)
        {
            var project = ConvertProject(request);
            var targetUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            _context.Update(targetUser);
            targetUser.Projects.Add(project);
            await _context.SaveChangesAsync();
            var projectResponse = ConvertToProjectResponse(project);
            return Created(Url.RouteUrl(projectResponse.Id), projectResponse);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetForUserName(string username)
        {
            var targetUser = await _context.Users
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(x => x.Username == username);
            return Ok(ConvertToProjectResponses(targetUser.Projects));
        }

        [HttpGet("[action]/{projectId}")]
        public async Task<IActionResult> GetForProjectId(int projectId)
        {
            var targetProject = await _context.Projects
                .Include(x=>x.Cards)
                .FirstOrDefaultAsync(x => x.Id == projectId);
            if (targetProject == null)
            {
                return BadRequest();
            }

            return Ok(ConvertToProjectResponse(targetProject));
        }
    }
}