using Dapper;
using MySqlConnector;

namespace APIPessoa.Repository
{
    public class PessoaRepository
    {
        public List<Pessoa> SelecionarPessoas()
        {
            string query = "SELECT * FROM Pessoa ORDER BY nome ASC";

            string stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

            using MySqlConnection conn = new(stringConnection);

            return conn.Query<Pessoa>(query).ToList();
        }

        public Pessoa SelecionarPessoa(string nome)
        {
            string query = $"SELECT * From Pessoa Where nome = '{nome}'";

            string stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

            using MySqlConnection conn = new(stringConnection);

            return conn.QueryFirstOrDefault<Pessoa>(query);
        }

        public bool InserirPessoa(Pessoa pessoa)
        {
            string query = "INSERT INTO Pessoa( nome, dataNascimento, idade, quantidadeFilhos )" +
                "VALUES (" + $"'{pessoa.Nome}','{pessoa.DataNascimento.ToString("yyyy-MM-dd")}', {pessoa.Idade},  {pessoa.QuantidadeFilhos})" ;

            string stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

            using MySqlConnection conn = new(stringConnection);

            int linhasAfetadas = conn.Execute(query);


            return linhasAfetadas > 0;
        }

        public bool AlterarPessoa(Pessoa pessoa, int id)
        {
            string query = $"UPDATE Pessoa SET nome = '{pessoa.Nome}', dataNascimento = '{pessoa.DataNascimento.ToString("yyyy-MM-dd")}', idade={pessoa.Idade}, quantidadeFilhos='{pessoa.QuantidadeFilhos}' Where id ={id}";

            string stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

            using MySqlConnection conn = new(stringConnection);

            int linhasAfetadas = conn.Execute(query);
            return linhasAfetadas > 0;
        }

        public bool DeletarPessoa(Pessoa pessoa, int id)
        {
            string query = $"DELETE FROM Pessoa WHERE id = {id}";

            string stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

            using MySqlConnection conn = new(stringConnection);

            int linhasAfetadas = conn.Execute(query);
            return linhasAfetadas > 0;

        }
    }
}
