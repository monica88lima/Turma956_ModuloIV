using ApiClientes.Core.Models;

namespace ApiClientes.Core.Interface
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> GetByCpf(string cpf);
        Task<bool> Insert(Cliente cliente);
        Task<bool> Update(Cliente cliente);
        Task<bool> Delete(long id);
    }
}
