using online_store.Models.NewFolder;

namespace online_store.Models
{
    public class UpdateAmountResponse //response for the object when the user clicks the button + or -
    {
        public ItemOrder ItemOrder { get; }

        public CarrinhoViewModel CarrinhoViewModel { get; }

        public UpdateAmountResponse(ItemOrder itemOrder, CarrinhoViewModel carrinhoViewModel)
        {
            ItemOrder = itemOrder;
            CarrinhoViewModel = carrinhoViewModel;
        }
    }
}
