namespace online_store.Models.NewFolder
{
    public class CarrinhoViewModel
    {
        public IList<ItemOrder> Items { get;}

        public CarrinhoViewModel(IList<ItemOrder> items)
        {
            Items = items;
        }

        public decimal Total => Items.Sum(_ => _.Quantidade * _.PrecoUnitario);
    }
}