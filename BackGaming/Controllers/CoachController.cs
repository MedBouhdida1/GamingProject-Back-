using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackGaming.Controllers
{
    [ApiController]
    [Route("api/coach")]
    public class CoachController : Controller

    {

        private readonly GamingApiDbContext dbContext;

        public CoachController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoachs()
        {
            return Ok(await dbContext.Coach.Include(c => c.Demandes).ToListAsync());
        }


       


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCoach([FromRoute] int id)
        {
            var user = await dbContext.Coach.FindAsync(id);
            if (user != null)
            {
                dbContext.Coach.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok("Success");
            }
            return Unauthorized("User not found");
        }



        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCoach(Coach coach, [FromRoute] int id)
        {
            var user = await dbContext.Coach.FindAsync(id);

            if (user != null)
            {
                user.FirstName = coach.FirstName;
                user.LastName = coach.LastName;
                user.Password = coach.Password;
                var checkmail = dbContext.Coach.Where(x => x.Email == coach.Email).FirstOrDefault();
                if ((checkmail != null && checkmail.Email == user.Email) || checkmail == null)
                {
                    user.Email = coach.Email;

                }
                else return Unauthorized("Email exist");
                await dbContext.SaveChangesAsync();
                return Ok(coach);
            }
            return NotFound("User not found");
        }

        [HttpGet("email/{email}")]
        public ActionResult<Coach> GetCoachByEmail(string email)
        {
            var user = dbContext.Coach.Where(u => u.Email == email).Include(d=>d.Demandes).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var client = await dbContext.Coach.Include(d=>d.Demandes).SingleOrDefaultAsync(d => d.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }



        [HttpPost]
        [Route("registerCoach")]
        public async Task<IActionResult> addClient([FromBody] Coach coach)
        {

            var cl = dbContext.Coach.Where(x => x.Email == coach.Email).FirstOrDefault();
            if (cl == null)
            {
                //Uncomment when adding coach from swagger
                //coach.Password = BCrypt.Net.BCrypt.HashPassword(coach.Password);

                await dbContext.Coach.AddAsync(coach);
                await dbContext.SaveChangesAsync();
                return Ok(coach);
            }
            return Unauthorized("Email exist");

        }
    }
}
