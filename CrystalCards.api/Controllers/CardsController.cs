using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrystalCards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        [HttpPost]
        public StatusCodeResult Post([FromBody] NewCardRequest request)
        {

            return StatusCode(201);
        }
    }
}