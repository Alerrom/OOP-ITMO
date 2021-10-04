using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private static int _counter = 0;
        private int _id;
        private Dictionary<string, (int price, int amount)> _items;

        public Shop(string name, string adress)
        {
            ShopAdress = adress ?? throw new InvalidShopNameException();
            ShopName = name ?? throw new InvalidShopNameException();
            _id = ++_counter;
            _items = new Dictionary<string, (int price, int amount)>();
        }

        public string ShopName { get; }
        public string ShopAdress { get; }
        public int ShopId => _id;

        public int GetItemPrice(Item item)
        {
            return _items[item.Name].price;
        }

        public int GetItemAmount(Item item)
        {
            return _items[item.Name].amount;
        }

        public void AddItem(List<Item> items, List<int> price, List<int> amount)
        {
            for (int i = 0; i < items.Count; ++i)
            {
                _items[items[i].Name] = (price[i], amount[i]);
            }
        }

        public void ChangeItemPrice(Item item, int newPrice)
        {
            _items[item.Name] = (newPrice, GetItemAmount(item));
        }

        public void AddItemWithBasicPrice(List<Item> items, List<int> amount)
        {
            for (int i = 0; i < items.Count; ++i)
            {
                _items[items[i].Name] = (GetItemPrice(items[i]), GetItemAmount(items[i]) + amount[i]);
            }
        }

        public bool EnoughItemInShop(Item item, int amount)
        {
            bool flag = false;
            foreach (string itemName in _items.Keys)
            {
                if (item.Name == itemName)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
                throw new FoundNotRegisteredItemException();

            return _items[item.Name].amount >= amount;
        }

        public void BuyItem(Item item, int amount)
        {
            _items[item.Name] = (GetItemPrice(item), GetItemAmount(item) - amount);
        }
    }
}