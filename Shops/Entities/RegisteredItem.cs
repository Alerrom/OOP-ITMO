using System.Threading;

namespace Shops.Entities
{
    public class RegisteredItem
    {
        private static int _counter = 0;
        private Item _item;
        private float _price;
        private int _amount;

        public RegisteredItem(Item item, float price, int amount)
        {
            _item = item;
            _price = price;
            _amount = amount;
            ItemId = ++_counter;
        }

        public int ItemId { get; }

        public string ItemName() => _item.Name;

        public float ItemPrice() => _price;

        public int ItemAmount() => _amount;

        public void SetNewPrice(float newPrice) => _price = newPrice;

        public void SetNewAmountAfterSupply(int newAmount) => _amount = newAmount + _amount;

        public void SetNewAmountAfterBuy(int newAmount) => _amount = newAmount - _amount;
    }
}