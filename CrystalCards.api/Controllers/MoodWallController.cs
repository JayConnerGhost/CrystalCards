using CrystalCards.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CrystalCards.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoodWallController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoodWallController(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}