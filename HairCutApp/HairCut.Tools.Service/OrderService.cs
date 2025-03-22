using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;

namespace HairCut.Tools.Service
{
    public class OrderService : IOrderService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IOrderRepository _orderRepository;
        private IUserRepository _userRepository { get; set; }
        private IItemRepository _itemRepository { get; set; }

        public OrderService(IConfiguration configuration, IOrderRepository orderRepository, IUserRepository userRepository, IItemRepository itemRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
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
                    throw new Exception("Sem itens encontrados");

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
    }
}
