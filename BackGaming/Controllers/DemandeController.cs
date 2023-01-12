using BackGaming.Data;
using BackGaming.Migrations;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BackGaming.Controllers
{
    [ApiController]
    [Route("api/demande")]
    public class DemandeController : Controller
    {
        private readonly GamingApiDbContext dbContext;



        public DemandeController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }





        [HttpGet]
        public async Task<IActionResult> GetDemandes()
        {
            return Ok(await dbContext.Demande.Include(c => c.Client).Include(d=>d.Coach).ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var demande =await dbContext.Demande
                .Include(d => d.Client).Include(d=>d.Coach)
                .SingleOrDefaultAsync(d => d.Id == id);
            if (demande == null)
            {
                return NotFound();
            }

            return Ok(demande);
        }


        [HttpPost]
        public async Task<IActionResult> addDemande([FromBody] Demande demande)
        {
           

            await dbContext.Demande.AddAsync(demande);
                await dbContext.SaveChangesAsync();
                return Ok(demande);
            

        }


        [HttpPut]
        [Route("etat/{id:int}")]
        public async Task<IActionResult> changeDemandeEtat(int id)
        {
            var demande = await dbContext.Demande.FindAsync(id);

            if (demande != null)
            {
                demande.etat = 1;
                
                await dbContext.SaveChangesAsync();
                return Ok(demande);
            }
            return NotFound("demande not found");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> modiDemande(int id, [FromBody] Demande demande)
        {
            var dem = await dbContext.Demande.FindAsync(id);

            if (demande != null)
            {
                dem.CoachId = demande.CoachId;

                await dbContext.SaveChangesAsync();
                return Ok(demande);
            }
            return NotFound("demande not found");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDemande([FromRoute] int id)
        {
            var demande = await dbContext.Demande.FindAsync(id);
            if (demande != null)
            {
                dbContext.Demande.Remove(demande);
                await dbContext.SaveChangesAsync();
                return Ok("Success");
            }
            return Unauthorized("User not found");
        }
    }
}
