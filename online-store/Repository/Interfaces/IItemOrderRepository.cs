using online_store.Models;

namespace online_store.Repository
{
    public interface IItemOrderRepository
    {
        ItemOrder GetItemOrder(int itemOrderId);
        void RemoveItemOrder(int itemOrderId);
   
    }
}