using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;

namespace HairCut.Tools.Service
{
    public class ItemService : IItemService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IItemRepository _itemRepository;
        private IUserRepository _userRepository { get; set; }
        private IProductRepository _productRepository { get; set; }

        public ItemService(IConfiguration configuration, IItemRepository itemRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _configuration = configuration;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> CreateAsync(ItemBase item, int userId)
        {
            try
            {
                var users = await _userRepository.FindByIdAsync(userId);

                if (users.Count == 0)
                    throw new Exception("Usuário não encontrado");

                var products = await _productRepository.FindByIdAsync(item.ItemId);

                if (products.Count == 0)
                    throw new Exception("Produto não disponível");

                if (item.Quantity > products[0].Total)
                    throw new Exception("Quantidade não disponível");

                item.UserId = userId;
                item.ItemName = products[0].Name;
                item.ItemPrice = products[0].Price;
                item.CreateDate = DateTime.Now;
                item.OrderRequestId = Guid.NewGuid();
                item.Status = ItemBase.ItemState.Pending;

                return await _itemRepository.InsertAsync(item);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<ItemResult>> FindByOrderIdAsync(Guid orderRequestId)
        {
            try
            {
                List<ItemResult> result = new List<ItemResult>();

                if (orderRequestId == Guid.Empty || orderRequestId == default)
                    throw new Exception("A key está vazia ou inválida");

                var items = await _itemRepository.FindByOrderIdAsync(orderRequestId);

                foreach (var it in items)
                {
                    ItemResult item = new ItemResult()
                    {
                        Name = it.ItemName,
                        Price = (double)it.ItemPrice,
                        Quantity = it.Quantity
                    };

                    if (it.Status == ItemBase.ItemState.Pending)
                        item.Status = "Pendente";

                    if (it.Status == ItemBase.ItemState.Waiting)
                        item.Status = "Aguardando";

                    if (string.IsNullOrEmpty(item.Status))
                        item.Status = "Finalizado";

                    result.Add(item);
                }

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid orderRequestId)
        {
            try
            {
                if (orderRequestId == Guid.Empty || orderRequestId == default)
                    throw new Exception("A key está vazia ou inválida");

                var items = await _itemRepository.FindByOrderIdAsync(orderRequestId);

                if (items.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                var result = await _itemRepository.DeleteAsync(items[0]);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
