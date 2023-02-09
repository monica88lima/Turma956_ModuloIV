using APIPessoa.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PessoaController : ControllerBase
    {
        public PessoaRepository _repository { get; set; }
        public PessoaController()
        {
            _repository = new PessoaRepository();
        }

        //ActionResult informa conteudo do body da resposta (response),
        //              utilizamos quando sabemos o conteudo de retorno;
        //IActionResult não informa conteudo do body da resposta (response),
        //              utilizamos quando não sabemos o conteudo exato
        //Ambos são informados com StatusCode;
        [HttpGet("todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Pessoa>> Consultar()
        {
            return Ok(_repository.SelecionarPessoas());
        }

        [HttpGet]
        public ActionResult<Pessoa> ConsultarPessoa(string nome)
        {
            return Ok(_repository.SelecionarPessoa(nome));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Pessoa> Inserir([FromBody] Pessoa pessoa)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            if (!_repository.InserirPessoa(pessoa))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ConsultarPessoa), pessoa);
        }

        [HttpPut("consultar/{index}/pessoa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Alterar([FromRoute] int index, [FromBody] Pessoa pessoa)
        {
            if (!_repository.AlterarPessoa(pessoa, index))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Deletar([FromQuery] int id)
        {
            if (!_repository.DeletarPessoa(id))
            {
                return BadRequest();
            }

            return NoContent();            
        }
    }
}