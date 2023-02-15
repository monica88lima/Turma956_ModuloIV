using APIProdutos.Core.Model;

namespace APIProdutos.Core.Interface
{
    public interface IProdutoService
    {
        List<Produto> ConsultarProdutos();
        bool InserirProduto(Produto produto);
        bool DeletarProduto(long id);
        bool AtualizarProduto(long id, Produto produto);
        Produto ConsultarProduto(string descricao);
        Produto ConsultarProduto(long id);
    }
}
