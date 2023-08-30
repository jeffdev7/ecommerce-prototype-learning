using Newtonsoft.Json;
using online_store.Models;

namespace online_store.Seeder
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context)
        {
            this._context = context;
        }

        public void SeederProducts()
        {
            List<Livro>? livros = GetBooks();

            foreach (var item in livros)
            {
                if (!livros.Contains(item))
                {
                    _context.Set<Product>().Add(new Product
                   (
                       item.Codigo, item.Nome, item.Preco
                   ));
                    _context.SaveChanges();

                }               
            }
        }
        private static List<Livro>? GetBooks()
        {
            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

    }
}
