using System.Text.Json;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ZooDbContext : DbContext
    {
        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Animal>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Enclosure)
                .WithMany(e => e.Animals)
                .HasForeignKey(a => a.EnclosureId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Enclosure>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Enclosure>()
                .HasMany(e => e.Animals)
                .WithOne(a => a.Enclosure)
                .HasForeignKey(a => a.EnclosureId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Enclosure>()
                .HasMany(e => e.Species)
                .WithOne(s => s.Enclosure)
                .HasForeignKey(s => s.EnclosureId);

            modelBuilder.Entity<Species>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Species>()
                .HasOne(s => s.Enclosure)
                .WithMany(e => e.Species)
                .HasForeignKey(s => s.EnclosureId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Enclosure>()
                .Property(e => e.Objects)
                .HasConversion(
                    obj => JsonSerializer.Serialize(obj, (JsonSerializerOptions)null),
                    json => JsonSerializer.Deserialize<List<string>>(json, (JsonSerializerOptions)null)
                );
        }
    }
}