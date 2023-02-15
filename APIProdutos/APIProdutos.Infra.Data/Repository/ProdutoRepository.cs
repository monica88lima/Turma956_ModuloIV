using APIProdutos.Core.Interface;
using APIProdutos.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIProdutos.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Produto> ConsultarProdutos()
        {
            var query = "SELECT * FROM Produtos";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Produto>(query).ToList();
        }
        public bool InserirProduto(Produto produto)
        {
            var query = "INSERT INTO Produtos VALUES (@descricao, @preco, @quantidade)";

            var parameters = new DynamicParameters(new
            {
                produto.Descricao,
                produto.Quantidade,
                produto.Preco
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
        public bool DeletarProduto(long id)
        {
            var query = "DELETE FROM Produtos WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
        public bool AtualizarProduto(Produto produto)
        {
            var query = @"UPDATE Produtos set descricao = @descricao,
                          preco = @preco, quantidade = @quantidade
                          where id = @id";

            var parameters = new DynamicParameters(produto);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }
        }
        public Produto ConsultarProduto(string descricao)
        {
            var query = "SELECT * FROM Produtos where descricao = @descricao";

            var parameters = new DynamicParameters(new
            {
                descricao
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Produto>(query, parameters);
        }
        public Produto ConsultarProduto(long id)
        {
            var query = "SELECT * FROM Produtos where id = @id";

            var parameters = new DynamicParameters(new
            {
                id
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Produto>(query, parameters);
        }
    }
}
