using Challenge.Alura.Adopet.API.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Data
{
    public class AdoPetContext:DbContext
    {
        public AdoPetContext(DbContextOptions<AdoPetContext> options)
            :base(options)
        {

        }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Abrigo> Abrigos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
