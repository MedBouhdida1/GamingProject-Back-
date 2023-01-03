using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackGaming.Controllers
{

    [ApiController]
    [Route("api/client")]
    public class ClientController : Controller
    {
        private readonly GamingApiDbContext dbContext;



        public ClientController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await dbContext.Client
                .Include(c => c.Demande)
                .ToListAsync();

            return Ok(clients);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var client = await dbContext.Client
                .Include(c => c.Demande)
                .SingleOrDefaultAsync(d => d.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> addClient([FromBody] Client client)
        {

            var cl = dbContext.Client.Where(x => x.Email == client.Email).FirstOrDefault();
            if (cl == null)
            {

                client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);
                await dbContext.Client.AddAsync(client);
                await dbContext.SaveChangesAsync();
                return Ok(client);
            }
            return Unauthorized("Email exist");

        }


        //Login for coach and client
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Client client)
        {
            var CoachUser = dbContext.Coach.Where(x => x.Email == client.Email).FirstOrDefault();
            if (CoachUser != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(client.Password, CoachUser.Password);
                if (verified)
                {
                    var claims = new[]
                    {
                     new Claim("Email", client.Email),
                     new Claim("Role","Coach")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: "https://example.com",
                        audience: "api1",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
                return Unauthorized("Wrong password");
            }
            var Clientuser = dbContext.Client.Where(x => x.Email == client.Email).FirstOrDefault();
            if (Clientuser != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(client.Password, Clientuser.Password);
                if (verified)
                {
                    var claims = new[]
                    {
                     new Claim("Email", client.Email),
                     new Claim("Role","Client")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: "https://example.com",
                        audience: "api1",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
                return Unauthorized("Wrong password");

            }
            return NotFound("user not found");
        }



        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            var user = await dbContext.Client.FindAsync(id);
            if (user != null)
            {
                dbContext.Client.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok("Success");
            }
            return Unauthorized("User not found");
        }




        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateClient( Client client, [FromRoute]int id)
        {
            var user =await dbContext.Client.FindAsync(id);

            if (user != null)
            {
                user.FirstName = client.FirstName;
                user.LastName=client.LastName;

                var checkmail = dbContext.Client.Where(x => x.Email == client.Email).FirstOrDefault();
                if ((checkmail != null && checkmail.Email == user.Email) || checkmail == null)
                {
                    user.Email = client.Email;

                }
                else return Unauthorized("Email exist");
                await dbContext.SaveChangesAsync();
                return Ok(client);
            }
            return NotFound("User not found");
        }

        [HttpGet("email/{email}")]
        public ActionResult<Client> GetClientByEmail(string email)
        {
            var user = dbContext.Client
                .Where(u => u.Email == email)
                .Include(u => u.Demande)
                .FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }

}



