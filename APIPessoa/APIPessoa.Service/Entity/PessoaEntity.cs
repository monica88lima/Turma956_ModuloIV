namespace APIPessoa.Service.Entity
{
    public class PessoaEntity
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }        
        public int QuantidadeFilhos { get; set; }
        public int Idade { get; set; }
        public string Permissao { get; set; }

    }
}