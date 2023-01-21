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

        public GamingApiDbContext()
        {
        }


        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Coach> Coach { get; set; }

        public virtual DbSet<Demande> Demande { get; set; }
        public virtual DbSet<Service> Service { get; set; }

        //useless function
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    Console.WriteLine("test execution funciton ! ");
        //    optionsBuilder.UseSqlServer("Server=LAPTOP-IS2DLEJ1;Database=GamingDB;User=sispoof;Password=root;Trusted_Connection=true;TrustServerCertificate=True; ");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Demande)
                .WithOne(d => d.Client)
                .HasForeignKey<Demande>(d => d.ClientId);
            modelBuilder.Entity<AchatService>()
               .HasKey(cs => new { cs.ClientId, cs.ServiceId });
            modelBuilder.Entity<AchatService>()
                .HasOne(cs => cs.Client)
                .WithMany(c => c.AchatServices)
                .HasForeignKey(cs => cs.ClientId);

            modelBuilder.Entity<AchatService>()
                .HasOne(cs => cs.Service)
                .WithMany(s => s.AchatServices)
                .HasForeignKey(cs => cs.ServiceId);
        }
    }
}
