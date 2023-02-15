using ApiClientes.Core.Interface;
using ApiClientes.Core.Models;
using Dapper;

namespace ApiClientes.Repository
{
    public class ClienteRepository : IClienteRepository
    {

        private readonly IConnectionDataBase _database;

        public ClienteRepository(IConnectionDataBase database)
        {
            _database = database;
        }
        public async Task<List<Cliente>> GetAllAsync()
        {
            var query = "SELECT * FROM clientes";
            using var conn = _database.CreateConnection();
            return (await conn.QueryAsync<Cliente>(query)).ToList();
        }

        public async Task<Cliente> GetByCpf(string cpf)
        {
            var query = "SELECT * FROM clientes where cpf = @cpf";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cpf);

            using var conn = _database.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Cliente>(query, parameters);
        }

        public async Task<bool> Insert(Cliente cliente)
        {
            var query = "INSERT INTO clientes values (@id, @cpf, @nome, @dataNascimento, @idade)";

            var parameters = new DynamicParameters(cliente);

            using var conn = _database.CreateConnection();
            return await conn.ExecuteAsync(query, parameters) > 0;
        }

        public async Task<bool> Update(Cliente cliente)
        {
            var query = @"UPDATE clientes SET CPF = @cpf, NOME = @nome, DATANASCIMENTO = @dataNascimento, IDADE = @idade
                          WHERE ID = @id";

            var parameters = new DynamicParameters(cliente);

            using var conn = _database.CreateConnection();
            return await conn.ExecuteAsync(query, parameters) > 0;
        }

        public async Task<bool> Delete(long id)
        {
            var query = "DELETE FROM CLIENTES WHERE ID = @id";

            var parameters = new DynamicParameters(new { id });

            using var conn = _database.CreateConnection();
            return await conn.ExecuteAsync(query, parameters) > 0;
        }
    }
}
