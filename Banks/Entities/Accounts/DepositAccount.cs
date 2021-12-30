using System;
using Banks.Entities.Clients;

namespace Banks.Entities.Accounts
{
    public class DepositAccount : AbstractAccount
    {
        public DepositAccount(float balance, Client owner, float interestOnBalance, TimeSpan interval)
            : base(balance, owner, interestOnBalance)
        {
            TimeInterval = interval;
        }

        public override float InterestOnBalance =>
            Balance switch
            {
                < 50000 => 3,
                < 100000 => 3.5f,
                >= 10000 => 4,
                _ => 0
            };

        public TimeSpan TimeInterval { get; private set; }

        public void UpdateTimeInterval(TimeSpan interval)
        {
            TimeInterval = interval;
        }
    }
}