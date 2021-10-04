using System.Collections.Generic;
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
            
            _shopManager.SupplyItems(
                shop,
                new List<Item>() {milk, chocolate},
                new List<int>() {10, 25},
                new List<int>() {60, 100});

            var customer1 = new Customer(2500);
            _shopManager.PurchaseItem(customer1, milk, 10);
            _shopManager.PurchaseItem(customer1, chocolate, 25);

            Assert.AreEqual(0, shop.GetItemAmount(milk));
            Assert.AreEqual(0, shop.GetItemAmount(chocolate));
        }

        [Test]
        public void ChangeItemPrice_RegisterItemAndChangePrice_PriceChanged()
        {
            Shop shop = _shopManager.CreateShop("Spar", "Пассаж");
            Item flakes = _shopManager.AddItem("MilPops");
            _shopManager.SupplyItems(
                shop,
                new List<Item>() {flakes},
                new List<int>() {5},
                new List<int>() {250});
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
            
            _shopManager.SupplyItems(
                shop1,
                new List<Item>() {milk},
                new List<int>() {5},
                new List<int>() {100});
            _shopManager.SupplyItems(
                shop2,
                new List<Item>() {milk},
                new List<int>() {5},
                new List<int>() {1000});
            _shopManager.SupplyItems(
                shop3,
                new List<Item>() {milk},
                new List<int>() {5},
                new List<int>() {100000});

            Shop ans = _shopManager.FindSuitableShop(milk, 1);
            Assert.AreEqual(100, ans.GetItemPrice(milk));
        }
        
        [Test]
        public void FindSuitableShops_CreateShopsWithDifferentPrices_ThrowNotEnoughAmountException()
        {
            Shop shop1 = _shopManager.CreateShop("Пятерочка", "Пассаж");
            Shop shop2 = _shopManager.CreateShop("Перекресток", "Пассаж");
            Shop shop3 = _shopManager.CreateShop("Spar", "Пассаж");
            Item milk = _shopManager.AddItem("Вологодское молоко");
            
            _shopManager.SupplyItems(
                shop1,
                new List<Item>() {milk},
                new List<int>() {1},
                new List<int>() {100});
            _shopManager.SupplyItems(
                shop2,
                new List<Item>() {milk},
                new List<int>() {1},
                new List<int>() {100});
            _shopManager.SupplyItems(
                shop3,
                new List<Item>() {milk},
                new List<int>() {1},
                new List<int>() {100});
            
            Assert.Catch<ShopException>(() => 
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
            
            _shopManager.SupplyItems(
                shop1,
                new List<Item>() {milk},
                new List<int>() {1},
                new List<int>() {100});
            _shopManager.SupplyItems(
                shop2,
                new List<Item>() {milk},
                new List<int>() {1},
                new List<int>() {100});
            _shopManager.SupplyItems(
                shop3,
                new List<Item>() {milk},
                new List<int>() {1},
                new List<int>() {100});

            Assert.Catch<ShopException>(() => 
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
            
            _shopManager.SupplyItems(
                shop,
                new List<Item>() {milk, chocolate},
                new List<int>() {10, 25},
                new List<int>() {60, 100});

            var customer1 = new Customer(600);
            _shopManager.PurchaseItem(customer1, milk, 10);

            Assert.AreEqual(0, shop.GetItemAmount(milk));
            Assert.AreEqual(0, customer1.Finance);
        }
    }
}