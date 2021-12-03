using System;
using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.TransactionSystem
{
    public class TransferFromCreditAccount : Transaction
    {
        public TransferFromCreditAccount(AbstractAccount sourceAccount, AbstractAccount destinationAccount)
            : base(sourceAccount, destinationAccount)
        {
        }

        public override void Transfer(float sum)
        {
            if (SourceAccount is CreditAccount creditAccount)
            {
                if (creditAccount.Balance - sum < -creditAccount.Limit)
                    throw new TransactionRejectedException("You have reached the limit");

                SourceAccount.Caretaker.Backup();
                DestinationAccount.Caretaker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else
            {
                TransactionChain.Transfer(sum);
            }
        }
    }
}