using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalCards.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrystalCards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Administrator")]
    public class UsersController : ControllerBase
    {

        public async Task<IActionResult> Get()
        {

            return Ok(new List<User>());
        }
    }
}