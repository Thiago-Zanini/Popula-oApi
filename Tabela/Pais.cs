using System.ComponentModel.DataAnnotations;

namespace Populacao.Tabela
{
    public class Pais
    {
        [Key]
        public int PaisId { get; set; }
        public required string Nome { get; set; }
    }
}
