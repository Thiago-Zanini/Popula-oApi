using System.ComponentModel.DataAnnotations;

namespace Populacao.Tabela
{
    public class Genero
    {
        [Key]
        public int GeneroId { get; set; }
        public required string Nome { get; set; }
    }
}
