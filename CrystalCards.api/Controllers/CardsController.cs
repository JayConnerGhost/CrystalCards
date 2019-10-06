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
                .Include(x=>x.ActionPoints)
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Cards.Update(entry);
            entry.Description = request.Description;
            entry.Title = request.Title;
            entry.Order = request.Order;
            ProcessPoints(_context, request.NPPoints, entry);
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
                .FirstOrDefaultAsync(x => x.Id == id);
            var resultConverted = ConvertResponse(result);
            return Ok(resultConverted);
        }


        private void ProcessActionPoints(ApplicationDbContext context, IList<ActionPointRequest> requestActionPoints, Card entity)
        {
            var actionPoints = ConvertActionPointRequests(requestActionPoints);
            var idPointToRemove = new List<int>();
            foreach (var entryPoints in entity.ActionPoints)
            {

                if (actionPoints.FirstOrDefault(x => x.Id == entryPoints.Id) == null)
                {
                    idPointToRemove.Add(entryPoints.Id);
                }
            }
            foreach (var id in idPointToRemove)
            {
                entity.ActionPoints.Remove(entity.ActionPoints.FirstOrDefault(x => x.Id == id));
            }

            //TODO...
            foreach (var point in actionPoints)
            {
                var noIdAssignedNewPoint = 0;
                //if new point
                if (point.Id == noIdAssignedNewPoint)
                {
                    entity.ActionPoints.Add(point);
                }
                else
                    //if point edited
                {
                    var positiveToUpdate = entity.ActionPoints.FirstOrDefault(x => x.Id == point.Id);
                    positiveToUpdate.Description = point.Description;
                }
            }

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
                    positiveToUpdate.Order = point.Order;
                }
            }
        }

        private List<ActionPoint> ConvertActionPointRequests(IList<ActionPointRequest> requestActionPoints)
        {
            var actionPoints = new List<ActionPoint>();
            foreach (var request in requestActionPoints)
            {
                actionPoints.Add(new ActionPoint()
                {
                    Id=request.Id,
                    Description = request.Description
                });
            }

            return actionPoints;
        }

        private List<CardResponse> ConvertResponses(List<Card> result)
        {
            var convertedResponses = new List<CardResponse>();
            foreach (var card in result)
            {
                convertedResponses.Add(ConvertResponse(card));
            }
           return convertedResponses;
        }

        private IList<NPPointResponse> ConvertPoints(IList<NPPoint> cardPoints)
        {
            var convertedResponses = new List<NPPointResponse>();
            foreach (var point in cardPoints)
            {
                var npPointResponse = new NPPointResponse
                {
                    Description = point.Description,
                    Id = point.Id,
                    Order = point.Order,
                    Direction = point.Direction == 0 ? "Positive" : "Negative"
                };
                convertedResponses.Add(npPointResponse);
            }

            return convertedResponses;
        }

        private CardResponse ConvertResponse(Card result)
        {
            return new CardResponse()
            {
                Description = result.Description,
                Title = result.Title,
                Id = result.Id,
                Order = result.Order,
                ActionPoints=ConvertActionPoints(result.ActionPoints),
                NPPoints = ConvertPoints(result.Points)

            };
        }

        private IList<ActionPointResponse> ConvertActionPoints(IList<ActionPoint> resultActionPoints)
        {
            var responses = new List<ActionPointResponse>();
            foreach (var actionPoint in resultActionPoints)
            {
                responses.Add(new ActionPointResponse()
                {
                    Id = actionPoint.Id,
                    Description=actionPoint.Description
                });
            }

            return responses;
        }

        private IEnumerable<NPPoint> ConvertPointRequests(IList<NPPointRequest> requestNpPoints)
        {
            return requestNpPoints.Select(npPointRequest => new NPPoint { Id = npPointRequest.Id, Direction = Enum.Parse<NPPointDirection>(npPointRequest.Direction), Description = npPointRequest.Description,Order=npPointRequest.Order }).ToList();
        }
    }
}