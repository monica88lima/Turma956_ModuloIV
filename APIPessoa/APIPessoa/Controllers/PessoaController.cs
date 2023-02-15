using APIPessoa.Filter;
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
        public IGerarTokenService _tokenService { get; set; }
        public PessoaController(IPessoaService pessoaService, IGerarTokenService tokenService)
        {
            _pessoaService = pessoaService;
            _tokenService = tokenService;
        }

        //ActionResult informa conteudo do body da resposta (response),
        //              utilizamos quando sabemos o conteudo de retorno;
        //IActionResult não informa conteudo do body da resposta (response),
        //              utilizamos quando não sabemos o conteudo exato
        //Ambos são informados com StatusCode;
        [HttpGet("todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> Consultar()
        {
            List<PessoaDto> pessoas = await _pessoaService.SelecionarPessoas();
            if (pessoas == null)
            {
                return BadRequest();
            }

            return Ok(pessoas);
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

        [HttpGet("Token")]
        public async Task<ActionResult<string>> GeraToken(string nome)
        {
            PessoaDto pessoa = await _pessoaService.SelecionarPessoa(nome);
            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(_tokenService.GerarTokenPessoa(pessoa.Nome, pessoa.Permissao));

        }

    }
}