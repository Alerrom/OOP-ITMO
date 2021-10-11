using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        public Customer(float finance)
        {
            Budget = finance;
        }

        public float Budget { get; set; }

        public void ChangeFinance(float budget)
        {
            if (Budget < budget)
                throw new ShopException();

            Budget -= budget;
        }
    }
}