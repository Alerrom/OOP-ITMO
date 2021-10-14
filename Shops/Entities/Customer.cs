using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private float _budget;
        public Customer(float finance)
        {
            _budget = finance;
        }

        public float Budget => _budget;

        public void CashWithdrawal(float budget)
        {
            if (_budget < budget)
                throw new BudgetLessThanZeroException();

            _budget -= budget;
        }

        public void Salary(float money)
        {
            _budget += money;
        }
    }
}