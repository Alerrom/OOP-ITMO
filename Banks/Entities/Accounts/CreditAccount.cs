using System;
using Banks.Entities.Clients;

namespace Banks.Entities.Accounts
{
    public class CreditAccount : AbstractAccount
    {
        private float _balance;
        public CreditAccount(float balance, Client owner, float interestOnBalance, float limit, float comission = 5f)
            : base(balance, owner, interestOnBalance)
        {
            Limit = limit;
            Comission = comission;
            _balance = balance;
        }

        public float Limit { get; }
        public float Comission { get; }

        public override float Balance
        {
            get => _balance < 0 ? _balance - (Comission / 100 * Math.Abs(_balance)) : _balance;
            set => _balance = value;
        }
    }
}