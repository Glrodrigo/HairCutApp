
namespace HairCut.Tools.Domain
{
    public class ItemBase : Entity
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }
        public Guid OrderRequestId { get; set; }
        public ItemState Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ConcludedDate { get; set; }

        public enum ItemState
        {
            Pending = 1,
            Waiting = 2,
            Concluded = 3
        }

        public ItemBase() { }

        public ItemBase(int itemId, int quantity)
        {
            if (itemId <= 0)
                throw new Exception("O id está em um formato inválido");

            if (quantity <= 0 || quantity > 99)
                throw new Exception("A quantia está em um formato inválido");

            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
