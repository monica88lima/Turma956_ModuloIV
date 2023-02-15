using APIProdutos.Core.Interface;
using APIProdutos.Core.Model;

namespace APIProdutos.Core.Service
{
    public class ProdutoService : IProdutoService
    {
        public IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public List<Produto> ConsultarProdutos()
        {
            return _produtoRepository.ConsultarProdutos();
        }
        public bool InserirProduto(Produto produto)
        {
            return _produtoRepository.InserirProduto(produto);
        }
        public bool DeletarProduto(long id)
        {
            return _produtoRepository.DeletarProduto(id);
        }
        public bool AtualizarProduto(long id, Produto produto)
        {
            try
            {
                produto = null;
                produto.Id = id;
                return _produtoRepository.AtualizarProduto(produto);
            }
            catch (NullReferenceException ex)
            {
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                var teste = ex.TargetSite;
                Console.WriteLine($"Valores nulos, mensagem {mensagem}, stack trace {caminho}, {teste}");

                return false;
            }
            catch (Exception ex)
            {
                var tipoExcecao = ex.GetType().Name;
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;

                Console.WriteLine($"Tipo da exceção {tipoExcecao}, mensagem {mensagem}, stack trace {caminho}");

                return false;

            }
            finally
            {
                Console.WriteLine("Bloco finally");
            }
        }
        public Produto ConsultarProduto(string descricao)
        {
            return _produtoRepository.ConsultarProduto(descricao);
        }
        public Produto ConsultarProduto(long id)
        {
            return _produtoRepository.ConsultarProduto(id);
        }
    }
}