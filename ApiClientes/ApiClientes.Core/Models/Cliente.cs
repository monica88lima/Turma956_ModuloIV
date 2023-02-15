using System.ComponentModel.DataAnnotations;

namespace ApiClientes.Core.Models
{
    public class Cliente
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public DateTime DataNascimento { get; set; }
        public int Idade { get; private set; }
        
        [Required]
        public string Permissao { get; set; }

        public Cliente(long id, string cpf, string nome, DateTime dataNascimento, int idade, string permissao)
        {
            Id = id;
            Cpf = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Idade = ObterIdade();
            Permissao = permissao;
        }

        public int ObterIdade()
        {
            int idade = DateTime.Now.Year - DataNascimento.Year;
            if (DateTime.Now.DayOfYear < DataNascimento.DayOfYear)
            {
                idade--;
            }
            return idade;
        }
    }
}