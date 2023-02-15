using ApiClientes.Core.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiClientes.Core.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateTokenProdutos(string nome, string permissao)
        {
            //Chave secreta para validação do Token
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("secretKey"));

            //Corpo do JWT
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "APIClientes.com", //Adicionando informação do issuer (quem gera o token)
                Audience = "APIEvents.com", //Adicionando informação do audience (quem recebe/utiliza o token)
                Expires = DateTime.UtcNow.AddHours(15), //Quanto tempo vai expirar o token
                Subject = new ClaimsIdentity(new Claim[] //Claims do usuario
                {
                    new Claim(ClaimTypes.Name, nome), //Claim de nome padrão
                    new Claim(ClaimTypes.Role, permissao), //Claim de role padrão
                    new Claim("teste", "123") //Claim personalizada
                }),
                SigningCredentials = new SigningCredentials( //Adicionando tipo de credencial
                    new SymmetricSecurityKey(key),           //Adicionando chave de validação do token
                    SecurityAlgorithms.HmacSha256Signature)  //Adicionando algoritmo de segurança do token
            };

            // Classe para manipular e gerar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //Criando a estrutura do token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serializa o token, transforma o token criado, criptografa
            return tokenHandler.WriteToken(token);
        }
    }
}
