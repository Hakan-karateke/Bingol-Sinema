using Microsoft.EntityFrameworkCore;

namespace BingolSinema.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        //public Kullanici Kullanici{get; set; }
        public DbSet<Bilet> Bilets{get; set; }
        public DbSet<Film> Films{get; set; }
        public DbSet<Kullanici> Kullanicis{get; set; }
        public DbSet<Rezervasyon> Rezervasyons{get; set; }
        public DbSet<Salon> Salons{get; set; }
        public DbSet<Seans> Seanss{get; set; }
        public DbSet<FilmTur> FilmTurs{get; set; }
        public DbSet<Admin> Admins{get; set; }
    }
}