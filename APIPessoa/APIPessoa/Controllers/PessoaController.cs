using Microsoft.AspNetCore.Mvc;

namespace APIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        public List<Pessoa> pessoas = new List<Pessoa>();
        public PessoaController()
        {
            pessoas.Add(new Pessoa
            {
                Nome = "Amanda",
                DataNascimento = new DateTime(1994, 05, 09)
            });
            pessoas.Add(new Pessoa
            {
                Nome = "Joaquim",
                DataNascimento = new DateTime(1968, 09, 17)
            });
        }

        [HttpGet]
        public IEnumerable<Pessoa> Consultar()
        {
            return pessoas;
        }

        [HttpPost]
        public void Inserir(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
        }

        [HttpPut]
        public IEnumerable<Pessoa> Alterar(int index, Pessoa pessoa)
        {
            pessoas[index] = pessoa;
            return pessoas;
        }

        [HttpDelete]
        public IEnumerable<Pessoa> Deletar(string nome)
        {
            Pessoa pessoaDeletar = pessoas.FirstOrDefault(x => x.Nome == nome);
            pessoas.Remove(pessoaDeletar);
            return pessoas;
        }
    }
}