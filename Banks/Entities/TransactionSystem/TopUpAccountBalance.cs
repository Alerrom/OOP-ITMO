using Banks.Entities.Accounts;

namespace Banks.Entities.TransactionSystem
{
    public class TopUpAccountBalance : Transaction
    {
        public TopUpAccountBalance(AbstractAccount sourceAccount)
            : base(sourceAccount)
        {
        }

        public override void TopUp(float sum)
        {
            SourceAccount.Caretaker.Backup();
            SourceAccount.Balance += sum;
        }
    }
}