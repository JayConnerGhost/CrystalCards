using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            var entry = await _context.Cards
                .Include(x=>x.Points)
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Cards.Update(entry);
            entry.Description = request.Description;
            entry.Title = request.Title;
            ProcessPoints(_context, request.NPPoints, entry);
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

            var entity = new Card(){Description=request.Description, Title = request.Title};
             ProcessPoints(_context, request.NPPoints, entity);
            var card= await _context.Cards.AddAsync(entity);
            _context.SaveChanges();


            return Created(Url.RouteUrl(card.Entity.Id),card.Entity);
        }

        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            var result = await _context.Cards
                .Include(x=>x.Points)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var result=await _context.Cards
                .Include(x=>x.Points)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(result);
        }

       

        private void ProcessPoints(ApplicationDbContext context, IList<NPPointRequest> requestNpPoints, Card entry)
        {
            var points = ConvertPointRequests(requestNpPoints);
            //if exists on card but not in points 
            //do find on points if not there then zap on card
            var idPointToRemove = new List<int>();
            foreach (var entryPoints in entry.Points)
            {

                if (points.FirstOrDefault(x => x.Id == entryPoints.Id) == null)
                {
                    idPointToRemove.Add(entryPoints.Id);
                }
            }

            foreach (var id in idPointToRemove)
            {
                entry.Points.Remove(entry.Points.FirstOrDefault(x => x.Id == id));
            }

            foreach (var point in points)
            {
                var noIdAssignedNewPoint = 0;
                //if new point
                if (point.Id == noIdAssignedNewPoint)
                {
                    entry.Points.Add(point);
                }
                else
                //if point edited
                {
                    var positiveToUpdate = entry.Points.FirstOrDefault(x => x.Id == point.Id);
                    positiveToUpdate.Direction = point.Direction;
                    positiveToUpdate.Description = point.Description;
                }
            }
        }

        private IEnumerable<NPPoint> ConvertPointRequests(IList<NPPointRequest> requestNpPoints)
        {
            return requestNpPoints.Select(npPointRequest => new NPPoint { Id = npPointRequest.Id, Direction = Enum.Parse<NPPointDirection>(npPointRequest.Direction), Description = npPointRequest.Description }).ToList();
        }

    }
}