using APIProdutos.Core.Model;

namespace APIProdutos.Core.Interface
{
    public interface IProdutoRepository
    {
        List<Produto> ConsultarProdutos();
        bool InserirProduto(Produto produto);
        bool DeletarProduto(long id);
        bool AtualizarProduto(Produto produto);
        Produto ConsultarProduto(string descricao);
        Produto ConsultarProduto(long id);
    }
}
