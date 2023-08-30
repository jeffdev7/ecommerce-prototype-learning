using online_store.Models;
using online_store.Repository.Interfaces;

namespace online_store.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Product> GetProducts()
        {
            return _context.Set<Product>().ToList();
        }
    }
}