using APIPessoa.Service.Dto;

namespace APIPessoa.Service.Interface
{
    public interface IPessoaService
    {
        Task<bool> InserirPessoa(PessoaDto pessoa);
        Task<List<PessoaDto>> SelecionarPessoas();
        Task<PessoaDto> SelecionarPessoa(string nome);
        Task<bool> AlterarPessoa(PessoaDto pessoa, int id);
        Task<bool> DeletarPessoa(int id);
    }
}
