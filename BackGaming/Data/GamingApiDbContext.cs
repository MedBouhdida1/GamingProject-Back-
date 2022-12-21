using BackGaming.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BackGaming.Data
{

    public class GamingApiDbContext : DbContext
    {
        public GamingApiDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Client> Client { get; set; }
    }
}
