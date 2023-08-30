using Microsoft.EntityFrameworkCore;
using online_store.Models;
using online_store.Models.NewFolder;

namespace online_store.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;//for cart[it saves items on the cart temporarily] so i can keep shopping
        private readonly IItemOrderRepository _itemOrderRepository;
        private readonly ICadastroRepository _cadastroRepository;
        public OrderRepository(ApplicationContext context, 
            IHttpContextAccessor contextAccessor,
            IItemOrderRepository itemOrderRepository,
            ICadastroRepository cadastroRepository) : base(context)
        {
            _contextAccessor = contextAccessor;
            _itemOrderRepository = itemOrderRepository;
            _cadastroRepository = cadastroRepository;
        }

        public void AddItem(string codigo)
        {
            var product = _context.Set<Product>()
                .Where(_ => _.Codigo == codigo)
                .SingleOrDefault();

            if(product == null)
            {
                throw new ArgumentException("Product not found.");
            }

            var order = GetOrder();
            var itemOrder = _context.Set<ItemOrder>()
                .Where(_ => _.Produto.Codigo == codigo
                &&  _.Pedido.Id == order.Id).SingleOrDefault();

            if(itemOrder == null)
            {
                itemOrder = new ItemOrder(order, product, 1, product.Preco);
                _context.Set<ItemOrder>().Add(itemOrder);

                _context.SaveChanges();
            }
        }

        public Order GetOrder()
        {
            var orderId = GetOrderId();
            var order = _dbSet.Include(_ => _.Itens)
                .ThenInclude(_ => _.Produto)
                .Include(_ => _.Cadastro)
                .Where(_ => _.Id == orderId).SingleOrDefault();

            if(order == null)
            {
                order = new Order();
                _dbSet.Add(order);
                _context.SaveChanges();
                SetOrderId(order.Id);
            }
            return order;
        }

        private int? GetOrderId()
        {
            return _contextAccessor.HttpContext.Session.GetInt32("orderId");//gravar pedido na sessao
            //for cart[it saves items on the cart temporarily] so i can keep shopping
        }

        private void SetOrderId(int orderId)
        {
            _contextAccessor.HttpContext.Session.SetInt32("orderId", orderId);
            //for cart[it saves items on the cart temporarily] so i can keep shopping
        }

        public UpdateAmountResponse UpdateAmountOfItems(ItemOrder itemOrder)
        {
            var cartViewModel = new CarrinhoViewModel(GetOrder().Itens);
            var itemOrderDb =
             _itemOrderRepository.GetItemOrder(itemOrder.Id);

            if (itemOrderDb != null)
            {
                itemOrderDb.UpdateAmount(itemOrder.Quantidade);

                if(itemOrder.Quantidade == 0)
                {
                    _itemOrderRepository.RemoveItemOrder(itemOrder.Id);
                }

                _context.SaveChanges();

                return new UpdateAmountResponse(itemOrderDb, cartViewModel);
            }

            throw new ArgumentException("ItemOrder was not found.");
        }

        public Order UpdateCadastro(Cadastro cadastro)
        {
            var order = GetOrder();
            var pedido = order.Cadastro.Id;

            _cadastroRepository.Update(pedido, cadastro);

            return order;
        }
    }
}