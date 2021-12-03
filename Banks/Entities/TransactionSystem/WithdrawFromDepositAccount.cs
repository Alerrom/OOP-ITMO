using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.TransactionSystem
{
    public class WithdrawFromDepositAccount : Transaction
    {
        public WithdrawFromDepositAccount(AbstractAccount sourceAccount)
            : base(sourceAccount)
        {
        }

        public override void Withdraw(float sum)
        {
            if (SourceAccount is DepositAccount depositAccount)
            {
                if (sum > depositAccount.Balance)
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