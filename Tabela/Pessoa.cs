using System.ComponentModel.DataAnnotations;

namespace Populacao.Tabela
{
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string NomeCompleto { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória")]
        [Range(18, 100, ErrorMessage = "A idade deve estar entre 18 a 100 anos")]
        public required int idade { get; set; }

        [Required(ErrorMessage = "GeneroId não pode ser vazio")]
        public required int GeneroId { get; set; }
        public required Genero Genero { get; set; }

        [Required(ErrorMessage = "PaisId não pode ser vazio")]
        public required int PaisId {  get; set; }
        public required Pais pais { get; set; }
    }
}
