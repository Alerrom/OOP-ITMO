using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
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
                if (varShop.ShopName == shopName && varShop.ShopAdress == shopAdress)
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

        public void SupplyItems(Shop shop, List<Item> items, List<int> amount, List<int> prices = default)
        {
            CheckValidOfSupplyItems(shop, items, prices, amount);

            if (prices == null)
            {
                foreach (Shop varShop in _shops)
                {
                    if (shop.ShopId == varShop.ShopId)
                    {
                        varShop.AddItemWithBasicPrice(items, amount);
                    }
                }
            }
            else
            {
                foreach (Shop varShop in _shops)
                {
                    if (shop.ShopId == varShop.ShopId)
                    {
                        varShop.AddItem(items, prices, amount);
                    }
                }
            }
        }

        public void PurchaseItem(Customer customer, Item item, int amount)
        {
            Shop shop = FindSuitableShop(item, amount);
            shop.BuyItem(item, amount);
            customer.Finance -= amount * shop.GetItemPrice(item);
        }

        public Shop FindSuitableShop(Item item, int amount)
        {
            var ans = new List<Shop>();
            foreach (Shop shop in _shops)
            {
                if (shop.EnoughItemInShop(item, amount))
                    ans.Add(shop);
            }

            Console.WriteLine(ans.Count);

            if (ans.Count == 0)
                throw new NotEnoughAmountException();

            var prices = new List<int>();
            foreach (Shop shop in ans)
            {
                prices.Add(shop.GetItemPrice(item) * amount);
            }

            int index = prices.FindIndex(x => x == prices.Min());

            return ans[index];
        }

        private Item FindItem(string itemName)
        {
            return _items.FirstOrDefault(item => item.Name == itemName);
        }

        private void CheckValidOfSupplyItems(Shop shop, List<Item> items, List<int> prices, List<int> amount)
        {
            if (prices == null && items.Count != amount.Count)
                throw new IncorrectSupplyInputException();
            if (prices == null)
                throw new IncorrectSupplyInputException();
            if (items.Count != prices.Count || items.Count != amount.Count || amount.Count != prices.Count)
                throw new IncorrectSupplyInputException();

            foreach (Item item in items)
            {
                if (FindItem(item.Name) == null)
                    throw new FoundNotRegisteredItemException();
            }
        }
    }
}