using CloudinaryDotNet.Actions;
using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using static HairCut.Tools.Domain.OrderResult;

namespace HairCut.Tools.Service
{
    public class OrderService : IOrderService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IOrderRepository _orderRepository;
        private IUserRepository _userRepository { get; set; }
        private IItemRepository _itemRepository { get; set; }
        private IProductRepository _productRepository { get; set; }

        public OrderService(IConfiguration configuration, IOrderRepository orderRepository, IUserRepository userRepository, 
            IItemRepository itemRepository, IProductRepository productRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> CreateAsync(int userId)
        {
            try
            {
                OrderBase orders = new OrderBase();

                var users = await _userRepository.FindByIdAsync(userId);

                if (users.Count == 0)
                    throw new Exception("Usuário não encontrado");

                var items = await _itemRepository.FindByUserIdAsync(userId, ItemBase.ItemState.Pending);

                if (items.Count == 0)
                    throw new Exception("Sem itens adicionados ao carrinho");

                foreach (var item in items)
                {
                    double total = item.Quantity * item.ItemPrice;
                    orders.Total = orders.Total + total;
                }

                orders.OrderRequestId = items.LastOrDefault().OrderRequestId;
                orders.UserId = userId;
                orders.Active = true;
                orders.Status = OrderBase.ItemState.Pending;
                orders.CreateDate = DateTime.Now;

                return await _orderRepository.InsertAsync(orders);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<OrderResult>> FindByIdAsync(int ordertId)
        {
            try
            {
                List<OrderResult> result = new List<OrderResult>();

                if (ordertId <= 0)
                    throw new Exception("A key está vazia ou inválida");

                var orders = await _orderRepository.FindByIdAsync(ordertId);

                foreach (var ord in orders)
                {
                    OrderResult order = new OrderResult()
                    {
                        OrderRequestId = ord.OrderRequestId,
                        Total = ord.Total
                    };

                    if (ord.Status == OrderBase.ItemState.Pending)
                        order.Status = "Pendente";

                    if (ord.Status == OrderBase.ItemState.Waiting)
                        order.Status = "Aguardando";

                    if (ord.Status == OrderBase.ItemState.Concluded)
                        order.Status = "Finalizado";

                    if (ord.ConcludedDate != null)
                        order.ConcludedDate = ((DateTime)ord.ConcludedDate).ToString("dd/MM/yyyy");

                    if (ord.ConcludedDate == null)
                        order.ConcludedDate = "-";

                    result.Add(order);
                }

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }


        public async Task<List<OrderResult>> FindByUserIdAsync(int userId)
        {
            try
            {
                List<OrderResult> result = new List<OrderResult>();

                if (userId <= 0)
                    throw new Exception("A key está vazia ou inválida");

                var orders = await _orderRepository.FindByUserIdAsync(userId);

                if (orders.Count == 0)
                    return result;

                var recent = orders.FirstOrDefault();

                OrderResult order = new OrderResult()
                {
                    OrderRequestId = recent.OrderRequestId,
                    Total = recent.Total
                };

                if (recent.Status == OrderBase.ItemState.Pending)
                    order.Status = "Pendente";

                if (recent.Status == OrderBase.ItemState.Waiting)
                    order.Status = "Aguardando";

                if (recent.Status == OrderBase.ItemState.Concluded)
                    order.Status = "Finalizado";

                if (recent.ConcludedDate != null)
                    order.ConcludedDate = ((DateTime)recent.ConcludedDate).ToString("dd/MM/yyyy");

                if (recent.ConcludedDate == null)
                    order.ConcludedDate = "-";

                result.Add(order);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<OrderTotalResults> GetByPageAsync(int pageNumber)
        {
            try
            {
                OrderTotalResults result = new OrderTotalResults();
                var pageSize = 20;

                if (pageNumber <= 0)
                    throw new Exception("A página está vazia ou inválida");

                var (orders, totalPages) = await _orderRepository.GetByPaginationAsync(pageNumber, pageSize);

                foreach (var ord in orders)
                {
                    OrderResult order = new OrderResult()
                    {
                        OrderRequestId = ord.OrderRequestId,
                        Total = ord.Total
                    };

                    if (ord.Status == OrderBase.ItemState.Pending)
                        order.Status = "Pendente";

                    if (ord.Status == OrderBase.ItemState.Waiting)
                        order.Status = "Aguardando";

                    if (ord.Status == OrderBase.ItemState.Concluded)
                        order.Status = "Finalizado";

                    if (ord.ConcludedDate != null)
                        order.ConcludedDate = ((DateTime)ord.ConcludedDate).ToString("dd/MM/yyyy");

                    if (ord.ConcludedDate == null)
                        order.ConcludedDate = "-";

                    result.Orders.Add(order);
                }

                if (orders.Count > 0)
                    result.TotalPages = totalPages;

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<OrderTotalResults> FindByStatusAsync(int pageNumber, OrderBase.ItemState status)
        {
            try
            {
                OrderTotalResults result = new OrderTotalResults();
                var pageSize = 20;

                if (pageNumber <= 0 || status < 0)
                    throw new Exception("A página ou a chave status está vazia ou inválida");

                var (orders, totalPages) = await _orderRepository.FindByStatusAsync(pageNumber, pageSize, status);

                foreach (var ord in orders)
                {
                    OrderResult order = new OrderResult()
                    {
                        OrderRequestId = ord.OrderRequestId,
                        Total = ord.Total
                    };

                    if (ord.Status == OrderBase.ItemState.Pending)
                        order.Status = "Pendente";

                    if (ord.Status == OrderBase.ItemState.Waiting)
                        order.Status = "Aguardando";

                    if (ord.Status == OrderBase.ItemState.Concluded)
                        order.Status = "Finalizado";

                    if (ord.ConcludedDate != null)
                        order.ConcludedDate = ((DateTime)ord.ConcludedDate).ToString("dd/MM/yyyy");

                    if (ord.ConcludedDate == null)
                        order.ConcludedDate = "-";

                    result.Orders.Add(order);
                }

                if (orders.Count > 0)
                    result.TotalPages = totalPages;

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            try
            {
                if (id == 0 || userId == 0)
                    throw new Exception("A key está vazia ou inválida");

                var orders = await _orderRepository.FindByIdAsync(id);

                if (orders.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                var order = orders[0];

                if (order.Active == false)
                    throw new Exception("A ordem não foi localizada em nossa base");

                order.Active = false;
                order.ExclusionDate = DateTime.UtcNow;
                order.EventDate = order.ExclusionDate;
                order.ChangeUserId = userId;

                var result = await _orderRepository.UpdateAsync(order);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id, int userId, OrderBase.ItemState status)
        {
            try
            {
                bool result = false;

                if (status != OrderBase.ItemState.Pending && status != OrderBase.ItemState.Waiting && status != OrderBase.ItemState.Concluded)
                    throw new Exception("O status está vazio ou inválido");

                if (id <= 0 || userId <= 0)
                    throw new Exception("A key está vazia ou inválida");

                var users = await _userRepository.FindByIdAsync(userId);

                if (users.Count == 0)
                    throw new Exception("Usuário não foi localizado em nossa base");

                var orders = await _orderRepository.FindByIdAsync(id, OrderBase.ItemState.Concluded);

                if (orders.Count == 0)
                    throw new Exception("A ordem não foi localizada em nossa base");

                var order = orders[0];

                if (order.Status != status)
                {
                    order.Status = status;
                    order.ChangeUserId = userId;
                    order.EventDate = DateTime.UtcNow;
                    order.ConcludedDate = null;

                    if (order.Status == OrderBase.ItemState.Concluded)
                        order.ConcludedDate = order.EventDate;

                    result = await _orderRepository.UpdateAsync(order);
                }

                if (result && order.Status == OrderBase.ItemState.Concluded)
                    await UpdateItems(userId, order);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private async Task<bool> UpdateItems(int userId, OrderBase order)
        {
            try
            {
                var result = false;
                var items = await _itemRepository.FindByStatusAndDateAsync(ItemBase.ItemState.Concluded, order.CreateDate, userId);

                foreach (var item in items)
                {
                    item.Status = ItemBase.ItemState.Concluded;
                    item.ConcludedDate = order.ConcludedDate;

                    result = await _itemRepository.UpdateAsync(item);
                }

                if (items?.Count > 0)
                    await UpdateProducts(items, order.ConcludedDate);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private async Task<bool> UpdateProducts(List<ItemBase> items, DateTime? concludedDate)
        {
            try
            {
                bool result = false;

                var itemQuantities = items
                    .GroupBy(i => i.ItemId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.OrderByDescending(i => i.CreateDate).First().Quantity
                    );

                foreach (var prod in itemQuantities)
                {
                    int productId = prod.Key;
                    int quantity = prod.Value;

                    var products = await _productRepository.FindByIdAsync(productId);

                    if (products.Count == 0)
                        continue;

                    var product = products[0];

                    product.Total = product.Total - quantity;
                    product.EventDate = concludedDate;

                    result = await _productRepository.UpdateAsync(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
