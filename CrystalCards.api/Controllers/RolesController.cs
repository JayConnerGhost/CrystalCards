using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CrystalCards.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : CustomControllerBase
    {
        
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public RolesController( IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody] RoleRemoveRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var targetUser = await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Username == request.Username.ToLower());
            _context.Update(targetUser);
            var customRole = targetUser.Roles.Find(x => x.Name.ToLower() == request.RoleName.ToLower());
            if (customRole != null)
            {
                targetUser.Roles.Remove(customRole);
            }

            await _context.SaveChangesAsync();

            return Ok(ConvertToUserResponse(targetUser));
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] RoleAssignmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var targetUser= await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Username == request.Username.ToLower());

            _context.Update(targetUser);
            targetUser.Roles.Add(new CustomRole(){Name=request.RoleName});
            await _context.SaveChangesAsync();

            return Ok(ConvertToUserResponse(targetUser));
        }

    }
}