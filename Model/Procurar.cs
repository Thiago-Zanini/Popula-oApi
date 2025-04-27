using System.ComponentModel.DataAnnotations;

namespace Populacao.Model
{
    public class Procurar
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string Nome { get; set; }
    }
}
