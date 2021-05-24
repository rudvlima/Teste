using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using knewinAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace knewinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CidadeController : ControllerBase
    {
        private readonly BancoContext _context;

        public CidadeController(BancoContext context)
        {
            _context = context;
        }

        /// GET api/Cidade/RetornarTodasCidades
        /// <summary>
        /// Retorna a Lista de Todas as Cidades Cadastradas.
        /// </summary>
        /// <returns>Lista de Cidades</returns>
        /// <response code="200">OK</response>
        [HttpGet("RetornarTodasCidades")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cidade>>> RetornarTodasCidades()
        {
            return await _context.Cidade.ToListAsync();
        }

        /// POST api/Cidade/CadastrarNovaCidade
        /// <summary>
        /// Cria uma Nova Cidade
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// Exemplo:
        ///
        ///     GET api/Cidade/CadastrarNovaCidade
        ///     {
        ///        "id": 1,
        ///        "nome": "cidade exemplo",
        ///        "habitantes": 2000,
        ///        "fronteira": [
        ///         "Cidade Fronteira"
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="cidade"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("CadastrarNovaCidade")]
        public async Task<ActionResult<Cidade>> CadastrarNovaCidade(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("RetornarCidadePorId", new { id = cidade.Id }, cidade);
        }

        /// GET api/Cidade/RetornarCidadePorNome
        /// <summary>
        /// Retorna uma determinada cidade atraves do nome.
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// </remarks>        
        /// <param name="nome"></param>
        /// <returns>Cidade</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Cidade não Encontrada</response>
        [HttpGet("RetornarCidadePorNome/{nome}")]
        public ActionResult<IEnumerable<Cidade>> RetornarCidadePorNome(string nome)
        {
            var Cidade = _context.Cidade.
                Where(e => e.Nome.ToLower().Contains(nome.ToLower())).ToList();

            if (Cidade == null || !Cidade.Any())
                return NotFound(new { message = "Cidade não Encontrada" });
            

            return Cidade;
        }


        /// GET api/Cidade/RetornarCidadePorId
        /// <summary>
        /// Retorna uma determinada cidade atraves do Identificador (Id).
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// </remarks>        
        /// <param name="id"></param>
        /// <returns>Cidade</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Cidade não Encontrada</response>
        [HttpGet("RetornarCidadePorId/{id}")]
        public async Task<ActionResult<Cidade>> RetornarCidadePorId(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);

            if (cidade == null)
                return NotFound(new { message = "Cidade não Encontrada" });

            return cidade;
        }

        /// GET api/Cidade/RetornarFronteirasPorCidade
        /// <summary>
        /// Retorna as fronteiras de uma cidade atraves de seu nome.
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// </remarks>        
        /// <param name="nome"></param>
        /// <returns>Cidade</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Fronteiras não Encontradas</response>
        [HttpGet("RetornarFronteirasPorCidade/{nome}")]
        public ActionResult<IEnumerable<Cidade>> RetornarFronteirasPorCidade(string nome)
        {
            var Cidade = _context.Cidade.ToList().
                Where(e => e.Fronteira.Any(b => b.ToLower().Contains(nome.ToLower()))).
                ToList();

            if (Cidade == null || !Cidade.Any())
                return NotFound(new { message = "Fronteiras não Encontradas" });

            return Cidade;
        }

        /// POST api/Cidade/RetornarSomaHabitantesPorCidades
        /// <summary>
        /// Retorna a soma do numero de habitantes de lista informada de cidades.
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// </remarks>        
        /// <param name="Cidades"></param>
        /// <returns>Soma de Habitantes</returns>
        /// <response code="200">OK</response>
        [HttpPost("RetornarSomaHabitantesPorCidades")]
        public IActionResult RetornarSomaHabitantesPorCidades(string[] Cidades)
        {
            var somaHabitantes = _context.Cidade.Where(x => Cidades.Select(y => y.ToLower()).Contains(x.Nome.ToLower())).Select(x => x.Habitantes).Sum();
             string nomeCidades = string.Join(',',Cidades.ToList());

            return Ok(new { Resultado= "as cidades '"+nomeCidades+"' possuem '"+somaHabitantes+"' habitantes" });
        }

        /// PUT api/Cidade/AlterarCidade
        /// <summary>
        /// Atualiza Informações da Cidade.
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// </remarks>        
        /// <param name="id"></param>
        /// <param name="cidade"></param>       
        /// <returns>Cidades</returns>
        /// <response code="200">OK</response>
        [HttpPut("AlterarCidade/{id}")]
        public async Task<IActionResult> AlterarCidade(int id, Cidade cidade)
        {
            if (id != cidade.Id)
                return BadRequest();

            _context.Entry(cidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadeExiste(id))
                {
                return NotFound(new { message = "Cidade não foi Alterada" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Valida se Cidade Existe atraves de Identificador (Id).
        /// </summary>
        /// <remarks>
        /// Necessita de Autenticação
        /// </remarks>        
        /// <param name="id"></param>
        /// <returns>true ou false</returns>
         private bool CidadeExiste(int id)
        {
            return _context.Cidade.Any(e => e.Id == id);
        }
    }
}
