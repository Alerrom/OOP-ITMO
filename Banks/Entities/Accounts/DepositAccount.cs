using System;
using Banks.Entities.Clients;

namespace Banks.Entities.Accounts
{
    public class DepositAccount : AbstractAccount
    {
        public DepositAccount(float balance, Client owner, float interestOnBalance, TimeSpan interval)
            : base(balance, owner, interestOnBalance)
        {
            Interval = interval;
        }

        public override float InterestOnBalance
        {
            get
            {
                return Balance switch
                {
                    < 50000 => 3,
                    < 100000 => 3.5f,
                    >= 100000 => 4,
                    _ => 0
                };
            }
        }

        public TimeSpan Interval { get; private set; }

        public void UpdateTimeInterval(TimeSpan interval)
        {
            Interval = interval;
        }
    }
}