using System;
using Banks.Entities.Clients;

namespace Banks.Entities.Accounts
{
    public class DebitAccount : AbstractAccount
    {
        public DebitAccount(float balance, Client owner, float interestOnBalance)
            : base(balance, owner, interestOnBalance)
        {
        }
    }
}