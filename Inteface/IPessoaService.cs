using Populacao.Model;
using Populacao.Tabela;

namespace Populacao.Inteface
{
    public interface IPessoaService
    {
        Task<string> Atualizar(Atualizar atualizar);
        Task<Resultado<List<Pessoa>>> Regiao(string Cache, string Nome);
        Task<Resultado<List<PessoaFiltro>>> Todos(string Cache, int Pagina, int Tamanho);
        Task<string> Add(Add Adicionar);
    }
}
