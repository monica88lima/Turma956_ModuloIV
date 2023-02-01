namespace APIPessoa
{
    public class Pessoa
    {
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public int Idade => DateTime.Now.AddYears(-DataNascimento.Year).Year;

    }
}