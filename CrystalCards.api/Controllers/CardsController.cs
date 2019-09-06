﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrystalCards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] NewCardRequest request)
        {
            
            await _context.Cards.AddAsync(new Card(){Description=request.Description, Title = request.Title});
            _context.SaveChanges();
            return StatusCode(201);
        }
    }
}