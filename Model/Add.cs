using Populacao.Tabela;
using System.ComponentModel.DataAnnotations;

namespace Populacao.Model
{
    public class Add
    {  
        public required string NomeCompleto { get; set; }
        public required int idade { get; set; }
        public required int GeneroId { get; set; }
        public required int PaisId { get; set; }
    }
}
