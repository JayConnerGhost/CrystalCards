using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrystalCards.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Net.Http.Headers;

namespace CrystalCards.Api.Controllers
{
    //Hack: Manual testing via postman for now, Mech. for automated test for file uploads not fully understood at this time KM 07102019  
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoodWallController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoodWallController(ApplicationDbContext context )
        {
            _context = context;
        }

        //this will eventually take a user id KM

        [HttpGet("{cardId}")]
        public IActionResult ReturnImageUrl(int cardId)
        {   
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), $@"Resources\images\{cardId.ToString()}");
            var fileNames = Directory.EnumerateFiles((folderPath)).Select(Path.GetFileName).ToList();
            return Ok(fileNames);
        }
        //This code is the simplist route - it need to be secured and expanded KM 07102019
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadImage()
        {
            try
            {
                //Work to be done here once authentication is inplace to make a path per user, for now doing simplist thing possible.
                var file = Request.Form.Files[0];
                var cardId = Request.Form["cardId"];
                var folderName = Path.Combine("Resources", "Images",cardId);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                Directory.CreateDirectory(pathToSave);

                //better safe guarding to go in here including but not limited to file extension checking
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                    var fullPath = Path.Combine(pathToSave, fileName.ToString());
                    var dbPath = Path.Combine(folderName.ToString(), fileName.ToString());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new {dbPath});
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}