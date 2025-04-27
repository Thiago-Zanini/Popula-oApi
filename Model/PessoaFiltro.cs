using Populacao.Tabela;

namespace Populacao.Model
{
    public class PessoaFiltro
    {
        public required string NomeCompleto { get; set; }
        public required int idade { get; set; }
        public required Genero Genero { get; set; }
        public required Pais pais { get; set; }
    }
}
