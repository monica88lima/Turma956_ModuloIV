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
        public List<Pessoa> pessoas = new List<Pessoa>();
        public PessoaController()
        {
            pessoas.Add(new Pessoa
            {
                Nome = "Amanda",
                DataNascimento = new DateTime(1994, 05, 09)
            });
            pessoas.Add(new Pessoa
            {
                Nome = "Joaquim",
                DataNascimento = new DateTime(1968, 09, 17)
            });
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
            PessoaRepository repository = new();

            List<Pessoa> pessoasBanco = repository.SelecionarPessoas();

            return Ok(pessoasBanco);
        }

        [HttpGet]
        public ActionResult<Pessoa> ConsultarPessoa(string nome)
        {
            PessoaRepository repository = new();

            Pessoa pessoaBanco = repository.SelecionarPessoa(nome);

            return Ok(pessoaBanco);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Pessoa> Inserir([FromBody] Pessoa pessoa)
        {
            PessoaRepository repository = new();

            repository.InserirPessoa(pessoa);
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            return CreatedAtAction(nameof(ConsultarPessoa), pessoa);
        }

        [HttpPut("consultar/{index}/pessoa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pessoa>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Alterar([FromRoute] int index, [FromBody] Pessoa pessoa)
        {
            PessoaRepository repository = new();

            if (index < 0)
            {
                return BadRequest();
            }

            Response.Headers.Add("rastreamento", "12345");
            repository.AlterarPessoa(pessoa, index);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Deletar(int index, Pessoa pessoa)
        {
            PessoaRepository repository = new();

            repository.DeletarPessoa(pessoa, index);
           
            return NoContent();

        }
    }
}