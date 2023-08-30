using online_store.Models;

namespace online_store.Repository
{
    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext context) : base(context)
        {
        }

        public Cadastro Update(int cadastroId, Cadastro novoCadastro)
        {
            var cadastroDb = _dbSet.Where(_ => _.Id == cadastroId)
                .SingleOrDefault();

            if(cadastroDb == null)
            {
                throw new ArgumentNullException(nameof(cadastroDb));
            }

            cadastroDb.Update(novoCadastro);
            _context.SaveChanges();
            return cadastroDb;
        }
    }
}