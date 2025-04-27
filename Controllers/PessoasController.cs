using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Populacao.Connection;
using Populacao.Inteface;
using Populacao.Model;
using Populacao.Tabela;

namespace Populacao.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly ILogger<PessoasController> logger;
        private readonly IMemoryCache memoryCache;
        private readonly IPessoaService pessoaService;

        private const string MemoryCacheTodos = nameof(MemoryCacheTodos);
        private const string MemoryCacheRegiao = nameof(MemoryCacheRegiao);

        public PessoasController(ILogger<PessoasController> logger, IMemoryCache memoryCache, IPessoaService pessoaService)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.pessoaService = pessoaService;
        }

        private void RemoveCache()
        {
            memoryCache.Remove(MemoryCacheTodos);
            memoryCache.Remove(MemoryCacheRegiao);
        }

        [EnableRateLimiting("Fixed")]
        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] Add add)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var resultado = await pessoaService.Add(add);

                if(resultado.Contains("não encontrado")) return NotFound(resultado);

                RemoveCache();

                return Ok(resultado);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [EnableRateLimiting("Fixed")]
        [HttpGet("Usuarios/Todos/{pagina}")]
        public async Task<IActionResult> Todos(int pagina = 1)
        {
            try
            {
                var Tamanho = 10;
                var CacheKey = $"{MemoryCacheTodos}_{pagina}";

                if (pagina < 1) return BadRequest(new {message = "Número de página invalido"});

                var usuarios = await pessoaService.Todos(CacheKey, pagina, Tamanho);

                if(!usuarios.Success) return NotFound(usuarios.Message);

                return Ok(usuarios.Dados);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [EnableRateLimiting("Fixed")]
        [HttpGet("Usuarios/Regiao/{Nome}")]
        public async Task<IActionResult> Regiao(string Nome)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(Nome)) return BadRequest(new {message = "Nome está vazio"});

                var cacheKey = $"{MemoryCacheRegiao}_{Nome}";
                
                var pessoas = await pessoaService.Regiao(cacheKey, Nome);

                if(!pessoas.Success) return NotFound(pessoas.Message);

                return Ok(pessoas.Dados);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [EnableRateLimiting("Fixed")]
        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] Atualizar atualizar)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await pessoaService.Atualizar(atualizar);

            if(resultado.Contains("erro")) return BadRequest(resultado);

            if(resultado.Contains("não encontrada")) return NotFound(new {message = resultado});

            return Ok(resultado);
        }
    }
}
