using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Populacao.Connection;
using Populacao.Inteface;
using Populacao.Model;
using Populacao.Tabela;

namespace Populacao.Configuracao
{
    public class PessoaService : IPessoaService
    {
        private readonly AppDbContext context;
        private readonly ILogger<PessoaService> logger;
        private readonly IMemoryCache memoryCache;
        public PessoaService(AppDbContext context, ILogger<PessoaService> logger, IMemoryCache memoryCache)
        {
            this.context = context;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        public async Task<string> Atualizar(Atualizar atualizar) 
        {
            try
            {
                var pessoa = await context.pessoas
                            .Include(p => p.Genero)
                            .Include(p => p.pais)
                            .Where(p => p.NomeCompleto == atualizar.Procurar)
                            .FirstOrDefaultAsync();

                if(pessoa == null) return "Pessoa não encontrada";

                var genero = await context.generos.FirstOrDefaultAsync(p => p.Nome == atualizar.GeneroA);
                var pais = await context.paises.FirstOrDefaultAsync(p => p.Nome == atualizar.PaisA);

                if (pais == null || genero == null) return "Pais ou Genero não encontrado";

                pessoa.NomeCompleto = atualizar.NomeCompleto;
                pessoa.idade = atualizar.idade;
                pessoa.GeneroId = genero.GeneroId;
                pessoa.PaisId = pais.PaisId;

                await context.SaveChangesAsync();

                return "Salvo com sucesso";
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return "Erro ao atualizar: " + ex.Message;
            }
        }

        public async Task<Resultado<List<Pessoa>>> Regiao(string Cache, string Nome)
        {
            try
            {
                if (!memoryCache.TryGetValue(Cache, out List<Pessoa>? pessoas))
                {
                    var Usuarios = await context.pessoas.AsNoTracking()
                                    .Include(p => p.Genero)
                                    .Include(p => p.pais)
                                    .Where(p => p.pais.Nome == Nome)
                                    .ToListAsync(); 

                    if (!Usuarios.Any())
                    {
                        return new Resultado<List<Pessoa>>
                        {
                            Success = false,
                            Message = "Pessoa não encontrada"
                        };
                    }

                    memoryCache.Set(Cache, Usuarios, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(60),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                    });

                    pessoas = Usuarios;
                }

                return new Resultado<List<Pessoa>>
                {
                    Success = true,
                    Dados = pessoas
                };
            }
            catch (Exception ex) 
            { 
                logger.LogError(ex.Message);
                return new Resultado<List<Pessoa>>
                {
                    Success = false,
                    Message = "Erro ao Procurar " + ex.Message
                };
            }
        }

        public async Task<Resultado<List<PessoaFiltro>>> Todos(string Cache, int Pagina, int Tamanho)
        {
            try
            {

                if (!memoryCache.TryGetValue(Cache, out List<PessoaFiltro>? filtros))
                {
                    var pessoa = await context.pessoas.AsNoTracking()
                                 .Include(p => p.Genero)
                                 .Include(p => p.pais)
                                 .Skip((Pagina - 1) * Tamanho)
                                 .Take(Pagina)
                                 .Select(p => new PessoaFiltro
                                 {
                                     NomeCompleto = p.NomeCompleto,
                                     idade = p.idade,
                                     Genero = p.Genero,
                                     pais = p.pais,
                                 }).ToListAsync();

                    if (!pessoa.Any())
                    {
                        return new Resultado<List<PessoaFiltro>>
                        {
                            Success = false,
                            Message = "Nenhuma pessoa encontrada"
                        };
                    }

                    memoryCache.Set(Cache, pessoa, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(60),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                    });

                    filtros = pessoa;
                }

                return new Resultado<List<PessoaFiltro>>
                {
                    Success = true,
                    Dados = filtros
                };

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Resultado<List<PessoaFiltro>>
                {
                    Success = false,
                    Message = "Erro ao procurar " + ex.Message
                };
            }
        }

        public async Task<string> Add(Add Adicionar)
        {
            try
            {
                
                var genero = await context.generos.FirstOrDefaultAsync(p => p.GeneroId == Adicionar.GeneroId);
                var pais = await context.paises.FirstOrDefaultAsync(p => p.PaisId == Adicionar.PaisId);

                if(genero == null || pais == null) return "genero ou pais não encontrado";

                var pessoa = new Pessoa
                {
                    NomeCompleto = Adicionar.NomeCompleto,
                    idade = Adicionar.idade,
                    GeneroId = Adicionar.GeneroId,
                    PaisId = Adicionar.PaisId,
                    Genero = genero,
                    pais = pais,
                };

                await context.pessoas.AddAsync(pessoa);
                await context.SaveChangesAsync();

                return "Adicionado com sucesso";

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return "Erro ao Adicionar " + ex.Message;
            }
        }
    }
}
