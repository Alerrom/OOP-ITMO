using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private static int _counter = 0;
        private int _id;
        private List<RegisteredItem> _items;

        public Shop(string name, string adress)
        {
            ShopAdress = adress ?? throw new InvalidShopNameException();
            ShopName = name ?? throw new InvalidShopNameException();
            _id = ++_counter;
            _items = new List<RegisteredItem>();
        }

        public string ShopName { get; }
        public string ShopAdress { get; }
        public int ShopId => _id;

        public List<RegisteredItem> GetRegisteredItems()
        {
            return _items;
        }

        public float GetItemPrice(Item item)
        {
            foreach (RegisteredItem registeredItem in _items)
            {
                if (registeredItem.ItemName() == item.Name)
                    return registeredItem.ItemPrice();
            }

            throw new FoundNotRegisteredItemException();
        }

        public int GetItemAmount(Item item)
        {
            foreach (RegisteredItem registeredItem in _items)
            {
                if (registeredItem.ItemName() == item.Name)
                    return registeredItem.ItemAmount();
            }

            throw new FoundNotRegisteredItemException();
        }

        public void AddItem(Item item, float price, int amount)
        {
            _items.Add(new RegisteredItem(item, price, amount));
        }

        public void ChangeItemPrice(Item item, int newPrice)
        {
            foreach (RegisteredItem registeredItem in _items)
            {
                if (registeredItem.ItemName() == item.Name)
                    registeredItem.SetNewPrice(newPrice);
            }
        }

        public void AddExtraItem(Item item, int amount)
        {
            foreach (RegisteredItem registeredItem in _items)
            {
                if (registeredItem.ItemName() == item.Name)
                {
                    registeredItem.SetNewAmountAfterSupply(amount);
                }
            }
        }

        public bool EnoughItemInShop(Item item, int amount)
        {
            bool flag = false;
            foreach (RegisteredItem registeredItem in _items)
            {
                if (item.Name != registeredItem.ItemName()) continue;
                flag = true;
                break;
            }

            if (!flag)
                throw new FoundNotRegisteredItemException();

            RegisteredItem tmp = null;
            foreach (RegisteredItem registeredItem in _items)
            {
                if (item.Name != registeredItem.ItemName())
                    continue;
                tmp = registeredItem;
                break;
            }

            return tmp.ItemAmount() >= amount;
        }

        public void BuyItem(Item item, int amount)
        {
            foreach (RegisteredItem registeredItem in _items)
            {
                if (registeredItem.ItemName() == item.Name)
                    registeredItem.SetNewAmountAfterBuy(amount);
            }
        }
    }
}