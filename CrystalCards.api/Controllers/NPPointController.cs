using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrystalCards.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NPPointController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewNPPointRequest request)
        {
            //check the card exists 
            //Update the NPPointTable



            return StatusCode(201, request);
        }
    }
}