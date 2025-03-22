using KütüphaneWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KütüphaneWebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Kitap> Kitap { get; set; }
        public DbSet<Destek> Destek { get; set; }
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
