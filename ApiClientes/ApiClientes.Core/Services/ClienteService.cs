using ApiClientes.Core.Interface;
using ApiClientes.Core.Models;

namespace ApiClientes.Core.Services
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }
        public async Task<Cliente> GetByCpf(string cpf)
        {
            return await _clienteRepository.GetByCpf(cpf);
        }
        public async Task<bool> Insert(Cliente cliente)
        {
            return await _clienteRepository.Insert(cliente);
        }
        public async Task<bool> Update(long id, Cliente cliente)
        {
            cliente.Id = id;
            return await _clienteRepository.Update(cliente);
        }
        public async Task<bool> Delete(long id)
        {
            return await _clienteRepository.Delete(id);
        }
    }
}
