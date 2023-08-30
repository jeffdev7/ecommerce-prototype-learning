using System.ComponentModel.DataAnnotations;

namespace online_store.Models
{
    public class Product : BaseModel
    {
        public Product()
        {

        }

        [Required]
        public string Codigo { get;  set; }
        [Required]
        public string Nome { get;  set; }
        [Required]
        public decimal Preco { get;  set; }

        public Product(string codigo, string nome, decimal preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}