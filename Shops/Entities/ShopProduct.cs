namespace Shops.Entities
{
    public class ShopProduct
    {
        private readonly Item _item;
        private float _price;
        private int _amount;

        public ShopProduct(Item item, float price, int amount)
        {
            _item = item;
            _price = price;
            _amount = amount;
        }

        public string Name() => _item.Name;

        public float Price() => _price;

        public int Amount() => _amount;

        public void SetNewPrice(float newPrice) => _price = newPrice;

        public void SetNewAmountAfterSupply(int newAmount) => _amount = newAmount + _amount;

        public void SetNewAmountAfterBuy(int newAmount) => _amount = newAmount - _amount;
    }
}