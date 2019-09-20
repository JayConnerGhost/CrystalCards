using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrystalCards.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NPPointController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NPPointController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewNPPointRequest request)
        {
            //check the card exists
            var parentCard = _context.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId);
            var parentCardResult = parentCard.Result;
            if (parentCardResult == null)
            {
                return BadRequest("No parent card");
            }
            //Update the NPPointTable
            _context.Cards.Update(parentCardResult);
            parentCardResult.Positives.Add(new NPPoint()
            {
                Direction = Enum.Parse<NPPointDirection>(request.Direction),
            });
           
            await _context.SaveChangesAsync();

            return StatusCode(201, request);
        }
    }
}