using Dapper;
using MySqlConnector;

namespace APIPessoa.Repository
{
    public class PessoaRepository
    {
        public List<Pessoa> SelecionarPessoas()
        {
            string query = "SELECT * FROM Pessoa";

            string stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

            using MySqlConnection conn = new(stringConnection);

            return conn.Query<Pessoa>(query).ToList();
        }
    }
}
