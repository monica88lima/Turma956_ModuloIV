using System.ComponentModel.DataAnnotations;

namespace APIPessoa
{
    public class Pessoa
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório!")]
        [MaxLength(255)]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        
        [Range(0, 20)]
        public int QuantidadeFilhos { get; set; }
        public int Idade => DateTime.Now.AddYears(-DataNascimento.Year).Year;

        //public Pessoa(string nome, DateTime dataNascimento, int quantidadeFilhos)
        //{
        //    Nome = nome;
        //    DataNascimento = dataNascimento;
        //    QuantidadeFilhos = quantidadeFilhos;
        //}
    }
}