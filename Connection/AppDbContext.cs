using Microsoft.EntityFrameworkCore;
using Populacao.Tabela;

namespace Populacao.Connection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pessoa> pessoas { get; set; }
        public DbSet<Genero> generos { get; set; }
        public DbSet<Pais> paises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genero>().HasData(
                new Genero { GeneroId = 1, Nome = "Masculino"},
                new Genero { GeneroId = 2, Nome = "Feminino"}
                );

            modelBuilder.Entity<Pais>().HasData(
                new Pais { PaisId = 1, Nome = "Brasil" },
                new Pais { PaisId = 2, Nome = "Estados Unidos" },
                new Pais { PaisId = 3, Nome = "Argentina" },
                new Pais { PaisId = 4, Nome = "Portugal" },
                new Pais { PaisId = 5, Nome = "França" },
                new Pais { PaisId = 6, Nome = "Alemanha" },
                new Pais { PaisId = 7, Nome = "Japão" },
                new Pais { PaisId = 8, Nome = "Itália" },
                new Pais { PaisId = 9, Nome = "Reino Unido" },
                new Pais { PaisId = 10, Nome = "Canadá" },
                new Pais { PaisId = 11, Nome = "México" },
                new Pais { PaisId = 12, Nome = "China" },
                new Pais { PaisId = 13, Nome = "Índia" },
                new Pais { PaisId = 14, Nome = "Egito" },
                new Pais { PaisId = 15, Nome = "África do Sul" },
                new Pais { PaisId = 16, Nome = "Rússia" },
                new Pais { PaisId = 17, Nome = "Austrália" },
                new Pais { PaisId = 18, Nome = "Espanha" },
                new Pais { PaisId = 19, Nome = "Paquistão" },
                new Pais { PaisId = 20, Nome = "Indonésia" }
                    );
        }
    }
}
