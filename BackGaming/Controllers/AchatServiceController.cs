using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackGaming.Controllers
{ 
    [ApiController]
    [Route("api/achatService")]
    public class AchatServiceController : Controller
    {
        private readonly GamingApiDbContext dbContext;
        public AchatServiceController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        [Route("buy")]
        public async Task<IActionResult> addAchatService([FromBody] AchatService achatService)
        {
                Console.WriteLine(achatService);
            await dbContext.AchatService.AddAsync(achatService);
            await dbContext.SaveChangesAsync();
            return Ok(achatService);



        }
        [HttpGet]
        public async Task<IActionResult> GetAchatService()
        {
            return Ok(await dbContext.AchatService.Include(c => c.Client).Include(s => s.Service).ToListAsync());
        }

    }       
}
