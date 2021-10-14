using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private static int _counter = 0;
        private List<ShopProduct> _items;

        public Shop(string name, string adress)
        {
            Adress = adress ?? throw new InvalidShopNameException();
            Name = name ?? throw new InvalidShopNameException();
            Id = ++_counter;
            _items = new List<ShopProduct>();
        }

        public string Name { get; }
        public string Adress { get; }
        public int Id { get; }

        public float GetItemPrice(Item item)
        {
            foreach (ShopProduct product in _items)
            {
                if (product.Name() == item.Name)
                    return product.Price();
            }

            throw new FoundNotRegisteredItemException();
        }

        public int GetItemAmount(Item item)
        {
            foreach (ShopProduct product in _items)
            {
                if (product.Name() == item.Name)
                    return product.Amount();
            }

            throw new FoundNotRegisteredItemException();
        }

        public void AddItem(Item item, float price, int amount)
        {
            if (IsItemInShop(item))
            {
                foreach (ShopProduct product in _items)
                {
                    if (product.Name() != item.Name) continue;
                    product.SetNewAmountAfterSupply(amount);
                    break;
                }
            }

            _items.Add(new ShopProduct(item, price, amount));
        }

        public void ChangeItemPrice(Item item, int newPrice)
        {
            foreach (ShopProduct product in _items)
            {
                if (product.Name() != item.Name) continue;
                product.SetNewPrice(newPrice);
                break;
            }
        }

        public bool EnoughItemInShop(Item item, int amount)
        {
            if (!IsItemInShop(item))
                throw new FoundNotRegisteredItemException();

            foreach (ShopProduct product in _items)
            {
                if (item.Name != product.Name())
                    continue;
                return product.Amount() >= amount;
            }

            return false;
        }

        public void BuyItem(Item item, int amount)
        {
            foreach (ShopProduct product in _items)
            {
                if (product.Name() == item.Name)
                {
                    product.SetNewAmountAfterBuy(amount);
                    break;
                }
            }
        }

        private bool IsItemInShop(Item item)
        {
            foreach (ShopProduct product in _items)
            {
                if (item.Name != product.Name()) continue;
                return true;
            }

            return false;
        }
    }
}