using Microsoft.EntityFrameworkCore;
using GestionBibliothequeAPI.Models;

namespace GestionBibliothequeAPI.Data
{
    public class BibliothequeContext : DbContext
    {
        public BibliothequeContext(DbContextOptions<BibliothequeContext> options) : base(options) { }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Livre)
                .WithMany()
                .HasForeignKey(e => e.LivreId);
        }
    }
}
