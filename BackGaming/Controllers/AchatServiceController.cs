using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackGaming.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class AchatServiceController : Controller
    {
        private readonly GamingApiDbContext dbContext;
        public AchatServiceController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> addAchatService([FromBody] AchatService achatService)
        {

                await dbContext.AchatService.AddAsync(achatService);
                await dbContext.SaveChangesAsync();
                return Ok(client);
         
            return Unauthorized("Email exist");

        }

    }
}
