using Populacao.Tabela;
using System.ComponentModel.DataAnnotations;

namespace Populacao.Model
{
    public class Atualizar
    {
        public required string Procurar {  get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string NomeCompleto { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória")]
        [Range(18, 100, ErrorMessage = "A idade deve estar entre 18 a 100 anos")]
        public required int idade { get; set; }

        [Required(ErrorMessage = "GeneroId não pode ser vazio")]
        public required string GeneroA { get; set; }

        [Required(ErrorMessage = "PaisId não pode ser vazio")]
        public required string PaisA { get; set; }
    }
}
