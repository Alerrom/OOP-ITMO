using System;
using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.TransactionSystem
{
    public class WithdrawFromCreditAccount : Transaction
    {
        public WithdrawFromCreditAccount(AbstractAccount sourceAccount)
            : base(sourceAccount)
        {
        }

        public override void Withdraw(float sum)
        {
            if (SourceAccount is CreditAccount creditAccount)
            {
                if (creditAccount.Balance - sum < -creditAccount.Limit)
                    throw new TransactionRejectedException("You have reached the limit");

                SourceAccount.Caretaker.Backup();
                SourceAccount.Balance -= sum;
            }
            else
            {
                TransactionChain.Transfer(sum);
            }
        }
    }
}