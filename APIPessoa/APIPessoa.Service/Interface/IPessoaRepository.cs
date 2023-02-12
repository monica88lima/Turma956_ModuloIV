using APIPessoa.Service.Entity;

namespace APIPessoa.Service.Interface
{
    public interface IPessoaRepository
    {
        Task<bool> InserirPessoa(PessoaEntity pessoa);
        Task<List<PessoaEntity>> SelecionarPessoas();
        Task<PessoaEntity> SelecionarPessoa(string nome);
        Task<bool> AlterarPessoa(PessoaEntity pessoa, int id);
        Task<bool> DeletarPessoa(int id);
    }
}
