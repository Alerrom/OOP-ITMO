using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private readonly List<Shop> _shops = new List<Shop>();
        private readonly List<Item> _items = new List<Item>();

        public Shop CreateShop(string shopName, string shopAdress)
        {
            foreach (Shop varShop in _shops)
            {
                if (varShop.Name == shopName && varShop.Adress == shopAdress)
                    throw new InvalidShopNameException();
            }

            var shop = new Shop(shopName, shopAdress);
            _shops.Add(shop);

            return shop;
        }

        public Item AddItem(string itemName)
        {
            if (FindItem(itemName) != null)
                throw new InvalidItemNameException();

            var item = new Item(itemName);
            _items.Add(item);
            return item;
        }

        public void SupplyItem(Shop shop, Item item, int amount, float price = 1.0f)
        {
            if (FindItem(item.Name) == null)
                throw new FoundNotRegisteredItemException();

            shop.AddItem(item, price, amount);
        }

        public void PurchaseItem(Customer customer, Item item, int amount)
        {
            Shop shop = FindSuitableShop(item, amount);
            shop.BuyItem(item, amount);
            customer.CashWithdrawal(amount * shop.GetItemPrice(item));
        }

        public Shop FindSuitableShop(Item item, int amount)
        {
            if (_shops.Count == 0)
                throw new ShopDoesNotExistException();

            Shop shop = null;
            foreach (Shop varShop in _shops)
            {
                if (varShop.EnoughItemInShop(item, amount) &&
                    (shop == null || shop.GetItemPrice(item) > varShop.GetItemPrice(item)))
                    shop = varShop;
            }

            if (shop == null)
                throw new NotEnoughAmountException();

            return shop;
        }

        private Item FindItem(string itemName)
        {
            return _items.FirstOrDefault(item => item.Name == itemName);
        }
    }
}