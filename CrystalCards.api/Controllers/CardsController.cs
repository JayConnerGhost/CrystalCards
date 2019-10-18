using System.Collections.Generic;
using System.Diagnostics;
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
            await _context.SaveChangesAsync();
            return Ok(ConvertResponse(entry));
        }

      
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = new Card(){Description=request.Description, Title = request.Title,Order=request.Order};
             ProcessPoints(_context, request.NPPoints, entity);
             ProcessActionPoints(_context, request.ActionPoints, entity);
             ProcessLinks(_context, request.Links, entity);
            var card= await _context.Cards.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Created(Url.RouteUrl(card.Entity.Id), ConvertResponse(card.Entity));
        }

       
        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            var result = await _context.Cards
                .Include(x=>x.Points)
                .Include(x=>x.ActionPoints)
                .Include(x => x.Links)
                .ToListAsync();
            var resultConverted = ConvertResponses(result);
            return Ok(resultConverted);
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