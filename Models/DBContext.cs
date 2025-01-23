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
        public DbSet<EtkinlikKatilim> EtkinlikKatilimlar { get; set; }

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
                .HasOne(e => e.ToplulukEntity)
                .WithMany(t => t.Etkinlikler)
                .HasForeignKey(e => e.Topluluk)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Katilim>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(k => k.Kullanici)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Katilim>()
                .HasOne<Topluluk>()
                .WithMany(t => t.Katilimlar)
                .HasForeignKey(k => k.Topluluk)
                .OnDelete(DeleteBehavior.Cascade);

            // EtkinlikKatilim ilişkileri
            modelBuilder.Entity<EtkinlikKatilim>()
                .HasOne(ek => ek.User)
                .WithMany()
                .HasForeignKey(ek => ek.KullaniciID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EtkinlikKatilim>()
                .HasOne(ek => ek.Etkinlik)
                .WithMany(e => e.Katilimlar)
                .HasForeignKey(ek => ek.EtkinlikID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}