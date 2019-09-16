using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class CardsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            _context.Cards.Update(entry);
            entry.Description = request.Description;
            entry.Title = request.Title;
            await _context.SaveChangesAsync();
            return Ok(entry);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var entry= await _context.Cards.AddAsync(new Card(){Description=request.Description, Title = request.Title});
            _context.SaveChanges();


            return Created(Url.RouteUrl(entry.Entity.Id),entry.Entity);
        }

        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            var result = await _context.Cards.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var result=await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(result);
        }
    }
}