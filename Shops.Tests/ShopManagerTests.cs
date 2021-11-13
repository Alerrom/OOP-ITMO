using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void SupplyItems_ShopCreatedItemsRegisteredAndDelivered_AbleToBuyItems()
        {
            Shop shop = _shopManager.CreateShop("Пятерочка", "Вязьма");
            Item milk = _shopManager.AddItem("Parmalat");
            Item chocolate = _shopManager.AddItem("Milka");
            
            _shopManager.SupplyItem(shop, milk, 10, 60.0f);
            _shopManager.SupplyItem(shop, chocolate, 25, 100.0f);

            Assert.AreEqual(10, shop.GetItemAmount(milk));
            Assert.AreEqual(25, shop.GetItemAmount(chocolate));
            
            var customer1 = new Customer(600);
            _shopManager.PurchaseItem(customer1, milk, 10);
            Assert.AreEqual(0.0f, customer1.Budget);
            customer1.Income(2500);
            _shopManager.PurchaseItem(customer1, chocolate, 25);
            Assert.AreEqual(0.0f, customer1.Budget);

            Assert.AreEqual(0, shop.GetItemAmount(milk));
            Assert.AreEqual(0, shop.GetItemAmount(chocolate));
            
            for (int i = 0; i < 20; ++i)
            {
                _shopManager.SupplyItem(shop,milk,1,20);
            }
            Assert.AreEqual(20,shop.GetItemAmount(milk));
        }

        [Test]
        public void ChangeItemPrice_RegisterItemAndChangePrice_PriceChanged()
        {
            Shop shop = _shopManager.CreateShop("Spar", "Пассаж");
            Item flakes = _shopManager.AddItem("MilPops");
            _shopManager.SupplyItem(shop, flakes, 250, 5.0f);
            shop.ChangeItemPrice(flakes, 500);
            
            Assert.AreEqual(500, shop.GetItemPrice(flakes));
        }

        [Test]
        public void FindSuitableShops_CreateShopsWithDifferentPrices_FoundTheBestOffer()
        {
            Shop shop1 = _shopManager.CreateShop("Пятерочка", "Пассаж");
            Shop shop2 = _shopManager.CreateShop("Перекресток", "Пассаж");
            Shop shop3 = _shopManager.CreateShop("Spar", "Пассаж");
            Item milk = _shopManager.AddItem("Вологодское молоко");
            
            _shopManager.SupplyItem(shop1, milk, 5, 100.0f);
            _shopManager.SupplyItem(shop2, milk, 5, 1000.0f);
            _shopManager.SupplyItem(shop3, milk, 5, 10000.0f);
            

            Shop ans = _shopManager.FindSuitableShop(milk, 1);
            Assert.AreEqual(100.0f, ans.GetItemPrice(milk));
        }
        
        [Test]
        public void FindSuitableShops_CreateShopsWithDifferentPrices_ThrowNotEnoughAmountException()
        {
            Shop shop1 = _shopManager.CreateShop("Пятерочка", "Пассаж");
            Shop shop2 = _shopManager.CreateShop("Перекресток", "Пассаж");
            Shop shop3 = _shopManager.CreateShop("Spar", "Пассаж");
            Item milk = _shopManager.AddItem("Вологодское молоко");
            
            _shopManager.SupplyItem(shop1, milk, 100, 10.0f);
            _shopManager.SupplyItem(shop2, milk, 100, 10.0f);
            _shopManager.SupplyItem(shop3, milk, 100, 10.0f);

            Assert.Catch<NotEnoughAmountException>(() => 
            {
                _shopManager.FindSuitableShop(milk, 200);
            });
        }
        
        [Test]
        public void FindSuitableShops_CreateItemWithoutRegistrationInSystem_ThrowFoundNotRegisteredItemException()
        {
            Shop shop1 = _shopManager.CreateShop("Пятерочка", "Пассаж");
            Shop shop2 = _shopManager.CreateShop("Перекресток", "Пассаж");
            Shop shop3 = _shopManager.CreateShop("Spar", "Пассаж");
            Item milk = _shopManager.AddItem("Вологодское молоко");
            var egg = new Item("Spar");
            
            _shopManager.SupplyItem(shop1, milk, 100, 10.0f);
            _shopManager.SupplyItem(shop2, milk, 100, 10.0f);
            _shopManager.SupplyItem(shop3, milk, 100, 10.0f);

            Assert.Catch<FoundNotRegisteredItemException>(() => 
            {
                _shopManager.FindSuitableShop(egg, 200);
            });
        }

        [Test]
        public void PurchaseItem_PurchaseConsignmentFromShop_CustomerFinanceChangedAndShopStockUpdated()
        {
            Shop shop = _shopManager.CreateShop("Магнит", "Купчино");
            Item milk = _shopManager.AddItem("Parmalat");
            Item chocolate = _shopManager.AddItem("Milka");

            _shopManager.SupplyItem(shop, milk, 10, 60.0f);
            _shopManager.SupplyItem(shop, chocolate, 25, 100.0f);

            var customer1 = new Customer(600);
            _shopManager.PurchaseItem(customer1, milk, 10);

            Assert.AreEqual(0, shop.GetItemAmount(milk));
            Assert.AreEqual(0.0f, customer1.Budget);
        }
    }
}