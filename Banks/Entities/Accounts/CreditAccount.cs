using Banks.Entities.Clients;

namespace Banks.Entities.Accounts
{
    public class CreditAccount : AbstractAccount
    {
        public CreditAccount(float balance, Client owner, float interestOnBalance, float limit)
            : base(balance, owner, interestOnBalance)
        {
            Limit = limit;
        }

        public float Limit { get; }
    }
}