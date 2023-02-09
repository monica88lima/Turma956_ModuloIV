using Dapper;
using MySqlConnector;

namespace APIPessoa.Repository
{
    public class PessoaRepository
    {
        private string _stringConnection { get; set; }
        public PessoaRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public List<Pessoa> SelecionarPessoas()
        {
            string query = "SELECT * FROM Pessoa";

            using MySqlConnection conn = new(_stringConnection);

            return conn.Query<Pessoa>(query).ToList();
        }

        //public List<Pessoa> SelecionarPessoa(string nome)
        //{
        //    string query = $"SELECT * FROM Pessoa where nome = '{nome}'";

        //    using MySqlConnection conn = new(_stringConnection);

        //    return conn.Query<Pessoa>(query).ToList();
        //}

        public List<Pessoa> SelecionarPessoa(string nome)
        {
            string query = $"SELECT * FROM Pessoa where nome = @nome";

            DynamicParameters param = new();
            param.Add("nome", nome);

            using MySqlConnection conn = new(_stringConnection);

            return conn.Query<Pessoa>(query, param).ToList();
        }

        public bool InserirPessoa(Pessoa pessoa)
        {
            string query = "INSERT INTO Pessoa (nome, dataNascimento, idade, quantidadeFilhos)" +
                "VALUES (@nome, @dataNascimento, @idade, @quantidadeFilhos)";

            DynamicParameters parametros = new(pessoa);

            //parametros.Add("nome", pessoa.Nome);
            //parametros.Add("dataNascimento", pessoa.DataNascimento);
            //parametros.Add("idade", pessoa.Idade);
            //parametros.Add("quantidadeFilhos", pessoa.QuantidadeFilhos);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = conn.Execute(query, parametros);

            return linhasAfetadas > 0;
        }

        public bool AlterarPessoa(Pessoa pessoa, int id)
        {
            string query = "UPDATE Pessoa SET nome = @nome, dataNascimento = @dataNascimento, idade = @idade," +
                "quantidadeFilhos = @quantidadeFilhos where id = @idPessoa";

            var parametros = new DynamicParameters(new
            {
                pessoa.Nome,
                pessoa.DataNascimento,
                pessoa.Idade,
                pessoa.QuantidadeFilhos,
                idPessoa = id
            });

            //var parametros = new DynamicParameters(pessoa);
            //parametros.Add("idPessoa", id);

            //parametros.Add("nome", pessoa.Nome);
            //parametros.Add("dataNascimento", pessoa.DataNascimento);
            //parametros.Add("idade", pessoa.Idade);
            //parametros.Add("quantidadeFilhos", pessoa.QuantidadeFilhos);
            //parametros.Add("idPessoa", id);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = conn.Execute(query, parametros);

            return linhasAfetadas > 0;
        }

        public bool DeletarPessoa(int id)
        {
            string query = "DELETE FROM Pessoa where id = @id";

            DynamicParameters parametros = new(id);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = conn.Execute(query, parametros);

            return linhasAfetadas > 0;
        } 


    }
}
