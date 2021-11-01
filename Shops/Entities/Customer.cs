using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        public Customer(float finance)
        {
            Budget = finance;
        }

        public float Budget { get; private set; }

        public void CashWithdrawal(float budget)
        {
            if (Budget < budget)
                throw new BudgetLessThanZeroException();

            Budget -= budget;
        }

        public void Income(float money)
        {
            Budget += money;
        }
    }
}