using APIProdutos.Core.Interface;
using APIProdutos.Core.Model;
using APIProdutos.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(LogResourceFilter))]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        public IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            Console.WriteLine("Instanciando Produto Controller");
            _produtoService = produtoService;
        }

        [HttpGet("/produto/{descricao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize("cliente,admin")]
        public ActionResult<Produto> GetProduto(string descricao)
        {
            var produtos = _produtoService.ConsultarProduto(descricao);
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpGet("/produto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("PolicyCors")]
        [Authorize(Roles = "admin")]
        public ActionResult<List<Produto>> ConsultarProdutos()
        {
            var teste = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "teste");

            Console.WriteLine($"Iniciando - {teste}");
            return Ok(_produtoService.ConsultarProdutos());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(LogActionFilter))]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            Console.WriteLine("Iniciando");
            if (!_produtoService.InserirProduto(produto))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostProduto), produto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [TypeFilter(typeof(LogActionFilter))]
        [ServiceFilter(typeof(GaranteProdutoExisteActionFilter))]
        public IActionResult UpdateProduto(long id, Produto produto)
        {
            Console.WriteLine("Iniciando");
            if (!_produtoService.AtualizarProduto(id, produto))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(GaranteProdutoExisteActionFilter))]
        [TypeFilter(typeof(LogAuthorizationFilter))]
        public ActionResult<List<Produto>> DeleteProduto(long id)
        {
            Console.WriteLine("Iniciando");
            if (!_produtoService.DeletarProduto(id))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}