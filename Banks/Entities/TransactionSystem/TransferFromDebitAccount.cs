using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.TransactionSystem
{
    public class TransferFromDebitAccount : Transaction
    {
        public TransferFromDebitAccount(AbstractAccount sourceAccount, AbstractAccount destinationAccount)
            : base(sourceAccount, destinationAccount)
        {
        }

        public override void Transfer(float sum)
        {
            if (SourceAccount is DebitAccount debitAccount)
            {
                if (sum > debitAccount.Balance)
                    throw new TransactionRejectedException("Insufficient funds on the account");

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