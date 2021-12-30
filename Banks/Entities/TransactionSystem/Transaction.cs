using Banks.Entities.Accounts;

namespace Banks.Entities.TransactionSystem
{
    public class Transaction : ITransactionChain
    {
        public Transaction(AbstractAccount sourceAccount)
        {
            SourceAccount = sourceAccount;
            DestinationAccount = null;
        }

        public Transaction(AbstractAccount sourceAccount, AbstractAccount destinationAccount)
        {
            SourceAccount = sourceAccount;
            DestinationAccount = destinationAccount;
        }

        public AbstractAccount DestinationAccount { get; }
        public AbstractAccount SourceAccount { get; }
        public ITransactionChain TransactionChain { get; private set; }

        public ITransactionChain SetNext(ITransactionChain transactionChain)
        {
            TransactionChain = transactionChain;
            return transactionChain;
        }

        public virtual void TopUp(float sum)
        {
            TransactionChain = new TopUpAccountBalance(SourceAccount);
            TransactionChain.TopUp(sum);
        }

        public virtual void Transfer(float sum)
        {
            TransactionChain = new TransferFromCreditAccount(SourceAccount, DestinationAccount);
            TransactionChain
                .SetNext(new TransferFromDebitAccount(SourceAccount, DestinationAccount))
                .SetNext(new TransferFromDepositAccount(SourceAccount, DestinationAccount));
            TransactionChain.Transfer(sum);
        }

        public virtual void Withdraw(float sum)
        {
            TransactionChain = new WithdrawFromCreditAccount(SourceAccount);
            TransactionChain
                .SetNext(new WithdrawFromDebitAccount(SourceAccount))
                .SetNext(new WithdrawFromDepositAccount(SourceAccount));
            TransactionChain.Withdraw(sum);
        }

        public void Undo()
        {
            SourceAccount.Caretaker.Undo();
            DestinationAccount?.Caretaker.Undo();
        }
    }
}