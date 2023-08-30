using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace online_store.Models
{
    [DataContract]
    public class ItemOrder : BaseModel
    {
        [Required]
        [DataMember]
        public Order Pedido { get;  set; }

        [Required]
        [DataMember]
        public Product Produto { get;  set; }

        [Required]
        [DataMember]
        public int Quantidade { get; set; }

        [Required]
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemOrder()
        {

        }
        public void UpdateAmount(int amount)
        {
            Quantidade = amount;
        }

        public ItemOrder(Order pedido, Product produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
    }
}