using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.TransactionSystem
{
    public class WithdrawFromDebitAccount : Transaction
    {
        public WithdrawFromDebitAccount(AbstractAccount sourceAccount)
            : base(sourceAccount)
        {
        }

        public override void Withdraw(float sum)
        {
            if (SourceAccount is DebitAccount debitAccount)
            {
                if (sum > debitAccount.Balance)
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