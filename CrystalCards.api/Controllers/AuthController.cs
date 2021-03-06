﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CrystalCards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        //Method here to check if a user exists


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]  UserForRegisterRequest userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();
             if (await _repo.UserExists(userForRegister.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                FirstName=userForRegister.FirstName,
                SecondName= userForRegister.SecondName,
                Username = userForRegister.Username
            };
            var createdUser = await _repo.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }

        [HttpGet("IsUserInSystem/{username}")]
        public async Task<IActionResult> IsUserInSystem(string username)
        {
            bool userExits= await _repo.UserExists(username);
            if (userExits)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginRequest userForLogin)
        {
           
            var userFromRepo = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),

                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
            claims.AddRange(userFromRepo.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name.ToString())));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken((tokenDescriptor));

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userFromRepo.Username
            });


        }
    }
}