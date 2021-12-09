namespace Shops.Entities
{
    public class ShopProduct
    {
        private readonly Item _item;

        public ShopProduct(Item item, float price, int amount)
        {
            _item = item;
            Price = price;
            Amount = amount;
        }

        public string Name { get => _item.Name; }

        public float Price { get; private set; }

        public int Amount { get; private set;  }

        public void SetNewPrice(float newPrice) => Price = newPrice;

        public void SetNewAmountAfterSupply(int newAmount) => Amount = newAmount + Amount;

        public void SetNewAmountAfterBuy(int newAmount) => Amount = newAmount - Amount;
    }
}