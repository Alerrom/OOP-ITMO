namespace Banks.Entities.TransactionSystem
{
    public interface ITransactionChain
    {
        public ITransactionChain SetNext(ITransactionChain transactionChain);
        public void TopUp(float sum);
        public void Transfer(float sum);
        public void Withdraw(float sum);
    }
}