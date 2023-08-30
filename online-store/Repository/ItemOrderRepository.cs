using online_store.Models;

namespace online_store.Repository
{
    public class ItemOrderRepository : BaseRepository<ItemOrder>, IItemOrderRepository
    {
        public ItemOrderRepository(ApplicationContext context) : base(context)
        {
        }

        public ItemOrder GetItemOrder(int itemOrderId)
        {
            return _dbSet.Where(_ => _.Id == itemOrderId)
                 .SingleOrDefault();
        }

        public void RemoveItemOrder(int itemOrderId)
        {
            _dbSet.Remove(GetItemOrder(itemOrderId));
            
        }
    }
}