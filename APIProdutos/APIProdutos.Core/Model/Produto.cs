using System.ComponentModel.DataAnnotations;

namespace APIProdutos.Core.Model
{
    public class Produto
    {
        public long Id { get; set; }

        [MinLength(2)]
        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }
    }
}