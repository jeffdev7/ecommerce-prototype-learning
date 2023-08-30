using Microsoft.AspNetCore.Mvc;
using online_store.Models;
using online_store.Models.NewFolder;
using online_store.Repository;
using online_store.Repository.Interfaces;
using X.PagedList;

namespace online_store.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IItemOrderRepository _itemOrderRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository,
            IItemOrderRepository itemOrderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _itemOrderRepository = itemOrderRepository;
        }

        public IActionResult Carrossel(int page = 1)
        {
            var products = _productRepository.GetProducts().OrderBy(_ => _.Id).ToPagedList(page, 12);
            var listOfProducts = _productRepository.GetProducts();
            return View(products);
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                _orderRepository.AddItem(codigo);
            }
            List<ItemOrder> orders = _orderRepository.GetOrder().Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(orders);
            return View(carrinhoViewModel);
        }
        public IActionResult Cadastro()
        {
            var order = _orderRepository.GetOrder();
            var pedido = order.Cadastro;

            if (order == null)
                return RedirectToAction("Carrossel");

            return View(pedido);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            
            if (!ModelState.IsValid)
            {
                Order order = _orderRepository.UpdateCadastro(cadastro);
                return View(order);
            }

            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateAmountResponse UpdateAmountOfItems([FromBody] ItemOrder itemOrder)
        {
            return _orderRepository.UpdateAmountOfItems(itemOrder);
        }
    }
}