namespace APIPessoa.Service.Interface
{
    public interface IGerarTokenService
    {
        string GerarTokenPessoa(string nome, string permissao);
    }
}
