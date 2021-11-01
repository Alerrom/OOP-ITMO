using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop CreateShop(string shopName, string shopAddress);
        Item AddItem(string itemName);

        void SupplyItem(Shop shop, Item item, int amount, float price);

        void PurchaseItem(Customer customer, Item item, int amount);

        Shop FindSuitableShop(Item item, int amount);
    }
}