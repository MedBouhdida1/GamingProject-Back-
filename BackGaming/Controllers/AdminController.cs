using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackGaming.Controllers
{
    [ApiController]
    [Route("api/admin")]


    public class AdminController : Controller
    {
        private readonly GamingApiDbContext dbContext;

        public AdminController(GamingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> addAdmin([FromBody] Admin admin)
        {
            var ad = dbContext.Admin.Where(x => x.Username == admin.Username).FirstOrDefault();
            if (ad == null)
            {
                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                await dbContext.Admin.AddAsync(admin);
                await dbContext.SaveChangesAsync();
                return Ok(admin);
            }
            return Unauthorized("Admin Existant already");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> adminLogin(Admin? ad)
        {
            var admin = dbContext.Admin.Where(x => x.Username == ad.Username).FirstOrDefault();
            if (admin != null)
            {
                if (BCrypt.Net.BCrypt.Verify(ad.Password, admin.Password))
                {
                    // generate token, for example using JWT
                    var claims = new[]
                      {
                     new Claim("Username", ad.Username)
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
            }
            return Unauthorized();
        }
    }
}
