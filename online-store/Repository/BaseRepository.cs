using Microsoft.EntityFrameworkCore;
using online_store.Models;

namespace online_store.Repository
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
    }
}