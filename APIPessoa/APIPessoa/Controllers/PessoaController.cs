using APIPessoa.Service.Dto;
using APIPessoa.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PessoaController : ControllerBase
    {
        public IPessoaService _pessoaService { get; set; }
        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        //ActionResult informa conteudo do body da resposta (response),
        //              utilizamos quando sabemos o conteudo de retorno;
        //IActionResult não informa conteudo do body da resposta (response),
        //              utilizamos quando não sabemos o conteudo exato
        //Ambos são informados com StatusCode;
        [HttpGet("todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> Consultar()
        {
            return Ok(await _pessoaService.SelecionarPessoas());
        }

        [HttpGet]
        public async Task<ActionResult<PessoaDto>> ConsultarPessoa(string nome)
        {
            return Ok(await _pessoaService.SelecionarPessoa(nome));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PessoaDto>> Inserir([FromBody] PessoaDto pessoa)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            if (!await _pessoaService.InserirPessoa(pessoa))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ConsultarPessoa), pessoa);
        }

        [HttpPut("consultar/{index}/pessoa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar([FromRoute] int index, [FromBody] PessoaDto pessoa)
        {
            if (!await _pessoaService.AlterarPessoa(pessoa, index))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Deletar([FromQuery] int id)
        {
            if (!await _pessoaService.DeletarPessoa(id))
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}