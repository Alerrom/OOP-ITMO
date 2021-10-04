using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop CreateShop(string shopName, string shopAdress);
        Item AddItem(string itemName);

        void SupplyItems(Shop shop, List<Item> items, List<int> amount, List<int> prices);

        void PurchaseItem(Customer customer, Item item, int amount);

        Shop FindSuitableShop(Item item, int amount);
    }
}