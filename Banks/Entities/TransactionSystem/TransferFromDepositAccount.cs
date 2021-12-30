using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.TransactionSystem
{
    public class TransferFromDepositAccount : Transaction
    {
        public TransferFromDepositAccount(AbstractAccount sourceAccount, AbstractAccount destinationAccount)
            : base(sourceAccount, destinationAccount)
        {
        }

        public override void Transfer(float sum)
        {
            if (SourceAccount is DepositAccount depositAccount)
            {
                if (sum > depositAccount.Balance)
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