using APIPessoa.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIPessoa.Service
{
    public class GerarTokenService : IGerarTokenService
    {
        public string GerarTokenPessoa(string nome, string permissao)
        {
            //Chave da variavel de ambiente, chave unica.
            var chaveCripto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

            //Corpo/ conteudo do JWT
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Issuer = "APIPessoa.com", // Informação sobre o emissor do token
                Audience = "SuaSaude.com", // Informação sobre o receptor/consumidor do token
                Expires = DateTime.UtcNow.AddHours(4), //Tempo de expiração do token
                //Claims do usuario, informacoes sobre a Pessoa
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, nome), // Claim com nome da pessoa 
                    new Claim(ClaimTypes.Role, permissao), // Claim com a permissao da pessoa
                    new Claim("teste", "1234") // Claim de teste personalizada
                }),
                SigningCredentials = new SigningCredentials( // Adiciona credencial
                    new SymmetricSecurityKey(chaveCripto), // Adiciona chave de criptografia do token
                    SecurityAlgorithms.HmacSha256Signature // Forma de criptografia do token
                    )
            };

            // Cria objeto para manipular o token -- gerenciador
            var tokenGerenciador = new JwtSecurityTokenHandler();

            // Gerenciador cria o token criptografado
            var token = tokenGerenciador.CreateToken(tokenDescricao);

            // Retorna token em forma de string criptografada
            return tokenGerenciador.WriteToken(token);
        }
    }
}
