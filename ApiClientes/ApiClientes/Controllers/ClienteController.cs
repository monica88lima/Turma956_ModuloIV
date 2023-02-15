using ApiClientes.Core.Interface;
using ApiClientes.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        public List<Cliente> clienteList = new()
        {
        };
        public IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("/Cliente/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> GetInfoCliente(string cpf)
        {
            var cliente = await _clienteService.GetByCpf(cpf);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("/Clientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Cliente>>> GetAllClients()
        {
            return Ok(await _clienteService.GetAllAsync());
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (!await _clienteService.Insert(cliente))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostCliente), cliente);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCliente(long id, Cliente cliente)
        {
            if (!await _clienteService.Update(id, cliente))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Cliente>>> DeleteCliente(long id)
        {
            if (!await _clienteService.Delete(id))
            {
                return NotFound();
            }
            return Ok(clienteList);
        }
    }
}