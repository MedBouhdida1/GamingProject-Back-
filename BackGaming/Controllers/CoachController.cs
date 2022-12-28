using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackGaming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(await dbContext.Coach.ToListAsync());
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

        [HttpGet("{email}")]
        public ActionResult<Coach> GetCoachByEmail(string email)
        {
            var user = dbContext.Coach.Where(u => u.Email == email).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }



        //Only for testing
        [HttpPost]
        [Route("registerCoach")]
        public async Task<IActionResult> addClient([FromBody] Coach coach)
        {

            var cl = dbContext.Coach.Where(x => x.Email == coach.Email).FirstOrDefault();
            if (cl == null)
            {

                coach.Password = BCrypt.Net.BCrypt.HashPassword(coach.Password);
                await dbContext.Coach.AddAsync(coach);
                await dbContext.SaveChangesAsync();
                return Ok(coach);
            }
            return Unauthorized("Email exist");

        }
    }
}
