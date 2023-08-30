using online_store.Models;

namespace online_store.Repository
{
    public interface IOrderRepository
    {
        void AddItem(string codigo);
        Order GetOrder();
        UpdateAmountResponse UpdateAmountOfItems(ItemOrder itemOrder);
        Order UpdateCadastro(Cadastro cadastro);
    }
}