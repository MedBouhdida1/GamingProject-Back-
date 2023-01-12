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


        public DbSet<Admin> Admin { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Coach> Coach { get; set; }

        public DbSet<Demande> Demande { get; set; }
        public DbSet<Service> Service { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-81T5ED7\\SQLEXPRESS;Database=GamingDb;Trusted_Connection=true;TrustServerCertificate=True; ");
        }
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
