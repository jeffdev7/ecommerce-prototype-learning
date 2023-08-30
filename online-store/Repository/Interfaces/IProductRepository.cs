using online_store.Models;

namespace online_store.Repository.Interfaces
{
    public interface IProductRepository
    {
        IList<Product> GetProducts();
    }
}