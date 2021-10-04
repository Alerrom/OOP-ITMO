using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        public Customer(int finance)
        {
            Finance = finance;
        }

        public int Finance { get; set; }

        public void ChangeFinance(int finance)
        {
            if (Finance < finance)
                throw new ShopException();

            Finance -= finance;
        }
    }
}