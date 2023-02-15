using ApiClientes.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        public IClienteService _clienteService;
        public ITokenService _tokenService;
        public TokenController(IClienteService clienteService, ITokenService tokenService)
        {
            _clienteService = clienteService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateToken(string cpf)
        {
            var client = await _clienteService.GetByCpf(cpf);
            if (client == null)
            {
                return BadRequest();
            }

            return Ok(_tokenService.GenerateTokenProdutos(client.Nome, client.Permissao));
        }
    }
}
