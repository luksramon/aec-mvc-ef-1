using aec_mvc_entity_framework.Models;
using Microsoft.EntityFrameworkCore;

namespace aec_mvc_entity_framework.Services 
{
    public class DbContexto : DbContext 
    {
        public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<Candidato>().HasIndex(u => u.Cpf).IsUnique();

        }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Profissao> Profissoes { get; set; }

    }
}