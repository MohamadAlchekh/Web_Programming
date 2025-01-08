using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Topluluk> Topluluklar { get; set; }
        public DbSet<Etkinlik> Etkinlikler { get; set; }
        public DbSet<Katilim> Katilimlar { get; set; }
        public DbSet<ToplulukOlusturmaIstegi> ToplulukOlusturmaIstekleri { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=WebProject;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Katilim tablosu için composite key tanımlama
            modelBuilder.Entity<Katilim>()
                .HasKey(k => k.ID);

            // İlişkileri tanımlama
            modelBuilder.Entity<Topluluk>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.Olusturan)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Etkinlik>()
                .HasOne<Topluluk>()
                .WithMany()
                .HasForeignKey(e => e.Topluluk)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Katilim>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(k => k.Kullanici)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Katilim>()
                .HasOne<Topluluk>()
                .WithMany()
                .HasForeignKey(k => k.Topluluk)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}