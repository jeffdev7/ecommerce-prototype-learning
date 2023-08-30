using System.ComponentModel.DataAnnotations;

namespace online_store.Models
{
    public class Order : BaseModel
    {
        public Order()
        {
            Cadastro = new Cadastro();
        }

        public Order(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        public List<ItemOrder> Itens { get; private set; } = new List<ItemOrder>();

        [Required]
        public int CadastroId { get; private set; }
        [Required]
        public virtual Cadastro Cadastro { get; private set; }
    }
}
