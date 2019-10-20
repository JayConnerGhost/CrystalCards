using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrystalCards.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : CustomControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CardsController> _logger;

        public CardsController(ApplicationDbContext context, ILogger<CardsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCardRequest request)
        {
            _logger.LogInformation($"in put controller {request} ");
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

            var entry = await _context.Cards
                .Include(x=>x.Points)
                .Include(x=>x.ActionPoints)
                .Include(x=>x.Links)
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Cards.Update(entry);
            entry.Description = request.Description;
            entry.Title = request.Title;
            entry.Order = request.Order;
            ProcessPoints(_context, request.NPPoints, entry);
            ProcessActionPoints(_context,request.ActionPoints,entry);
            ProcessLinks(_context, request.Links, entry);
            await _context.SaveChangesAsync();
            return Ok(ConvertResponse(entry));
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> PostWithUserName([FromBody] NewCardRequest request, string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //get user 
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            var entity = new Card() { Description = request.Description, Title = request.Title, Order = request.Order };
            ProcessPoints(_context, request.NPPoints, entity);
            ProcessActionPoints(_context, request.ActionPoints, entity);
            ProcessLinks(_context, request.Links, entity);
            var userEntity = _context.Users.Update(user);
            user.Cards.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(Guid.NewGuid().ToString(),e);
            }
          

            return Created(Url.RouteUrl(entity.Id), ConvertResponse(entity));
        }

        [HttpGet("[action]/{username}")]
        public async Task<OkObjectResult> GetForUserName(string username)
        {
            User user;
            List<CardResponse> convertResponses=null;
            try
            {
                user = await _context.Users
                    .Include(x => x.Cards)
                    .ThenInclude(cs=>cs.ActionPoints)
                    .Include(cs=>cs.Cards)
                    .ThenInclude(cs3=>cs3.Points)
                    .Include(cs4=>cs4.Cards).ThenInclude(cs5=>cs5.Links)
                    .FirstOrDefaultAsync(x => x.Username == username);
                List<Card> cards = (List<Card>)user.Cards;
                convertResponses = ConvertResponses(cards);
            }
            catch (Exception e)
            {
               _logger.LogError(Guid.NewGuid().ToString(),e);
            }
           

         
            return Ok(convertResponses);

        }

        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var result=await _context.Cards
                .Include(x=>x.Points)
                .Include(x=>x.ActionPoints)
                .Include(x => x.Links)
                .FirstOrDefaultAsync(x => x.Id == id);
            var resultConverted = ConvertResponse(result);
            return Ok(resultConverted);
        }


     
    }
}