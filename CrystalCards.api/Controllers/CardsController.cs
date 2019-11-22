using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICardRepository _repository;

        public CardsController(ApplicationDbContext context, ILogger<CardsController> logger, ICardRepository repository)
        {
            _context = context;
            _logger = logger;
            _repository = repository;
        }


        //NOTE: to fix code in here to clean up a cards images 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result=await _repository.Delete(id);
            switch (result)
            {
                case OpsStatus.Failed:
                    return StatusCode(500);
                case OpsStatus.TargetNotFound:
                    return BadRequest();
                default:
                    return StatusCode(204);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCardRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _repository.Get(id);

            entity.Description = request.Description;
            entity.Title = request.Title;
            entity.Order = request.Order;
            ProcessPoints(_context, request.NPPoints, entity);
            ProcessActionPoints(_context, request.ActionPoints, entity);
            ProcessLinks(_context, request.Links, entity);

            var card=await _repository.Update(entity);
            return Ok(ConvertResponse(card));
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> PostWithUserName([FromBody] NewCardRequest request, string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var entity = new Card() { Description = request.Description, Title = request.Title, Order = request.Order };
            ProcessPoints(_context, request.NPPoints, entity);
            ProcessActionPoints(_context, request.ActionPoints, entity);
            ProcessLinks(_context, request.Links, entity);
      
            try
            {
                var card = _repository.Add(entity, username);
            }
            catch (Exception e)
            {
                _logger.LogError(Guid.NewGuid().ToString(), e);
            }


            return Created(Url.RouteUrl(entity.Id), ConvertResponse(entity));
        }

        [HttpGet("[action]/{username}")]
        public async Task<OkObjectResult> GetForUserName(string username)
        {
            User user;
            List<CardResponse> convertResponses = null;
            try
            {
                user = await _context.Users
                    .Include(x => x.Cards)
                    .ThenInclude(cs => cs.ActionPoints)
                    .Include(cs => cs.Cards)
                    .ThenInclude(cs3 => cs3.Points)
                    .Include(cs4 => cs4.Cards).ThenInclude(cs5 => cs5.Links)
                    .FirstOrDefaultAsync(x => x.Username == username);
                List<Card> cards = (List<Card>)user.Cards;
                convertResponses = ConvertCardResponses(cards);
            }
            catch (Exception e)
            {
                _logger.LogError(Guid.NewGuid().ToString(), e);
            }

            return Ok(convertResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var  result  = await _repository.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            var resultConverted = ConvertResponse(result);
            return Ok(resultConverted);
        }

    }
}