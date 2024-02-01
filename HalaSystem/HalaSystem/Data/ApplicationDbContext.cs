using HalaSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HalaSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Hala> Hale { get; set; }
        public DbSet<Rezerwacja> Rezerwacje { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}