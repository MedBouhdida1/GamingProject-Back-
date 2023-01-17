using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackGaming.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : Controller
    {
        private readonly GamingApiDbContext dbContext;

        public ServiceController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await dbContext.Service.Include(c => c.Coach).ToListAsync();
            return services.Count == 0 ? NotFound("No services found!") : Ok(services);
        }

        [HttpGet]
        [Route("serviceByCoachId/{id:int}")]
        public async Task<IActionResult> getServiceByCoach(int coachId)
        {
            var servicesByCoach = await dbContext.Service.Where(c => c.CoachId == coachId).ToListAsync();
          
            return servicesByCoach.Count == 0 ? NotFound("Coach has no Service yet") : Ok(servicesByCoach);
        }


        [HttpPost]
        [Route("add/{coachId:int}")]
        public async Task<IActionResult> addByCoachIdService([FromBody] Service service,int coachId)
        {
            Console.WriteLine(coachId);
            service.CoachId=coachId;
             List<Demande> getStateDemande = await dbContext.Demande.Where(d => d.CoachId == coachId).ToListAsync();
            if (getStateDemande[0].etat != 1) return StatusCode(406); 
            try {
                await dbContext.Service.AddAsync(service);
                await dbContext.SaveChangesAsync();
                return Ok(service);
            }
            catch(Exception ex)
            {
                return BadRequest("error while saving data to db");
            }
        }
    }
}
