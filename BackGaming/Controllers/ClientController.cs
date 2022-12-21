using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.ObjectPool;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace BackGaming.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ClientController:Controller
    {

        private readonly GamingApiDbContext dbContext;

        public ClientController(GamingApiDbContext dbContext) 
            {
                this.dbContext = dbContext;
            }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await dbContext.Client.ToListAsync());
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> addClient(Client client)
        {

            var cl = dbContext.Client.Where(x => x.Email == client.Email).FirstOrDefault();
            if(cl == null)
            {
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(client.Password);
                byte[] enteredHashBytes = SHA256.Create().ComputeHash(enteredPasswordBytes);
                string enteredHashedPassword = Convert.ToBase64String(enteredHashBytes);
                client.Password = enteredHashedPassword;
                await dbContext.Client.AddAsync(client);
                await dbContext.SaveChangesAsync();
                return Ok(client);
            }
            return Problem("Email exist");
         
        }


       

        
    }
}
