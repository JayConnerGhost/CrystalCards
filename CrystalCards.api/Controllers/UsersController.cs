using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrystalCards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Administrator")]
    public class UsersController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ApplicationDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .Include(x=>x.Cards)
                .Include(x=>x.Roles)
                .ToListAsync();
          
            return Ok(ConvertToUserResponses(users));
        }

        [HttpGet("{Username}")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _context.Users
                .Include(x => x.Cards)
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Username == username.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            return Ok(ConvertToUserResponse(user));
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await _context.Users
                .Include(x => x.Cards)
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Username == username.ToLower());
            _context.Remove(user);
            await _context.SaveChangesAsync();

            return Accepted();
        }
    }
}