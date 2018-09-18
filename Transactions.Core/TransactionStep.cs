namespace Transactions.Core
{
    public abstract class TransactionStep : ITransactionStep
    {
        public virtual void BeforeExecution()
        {
        }

        public abstract void Execute();

        public virtual void AfterExecution()
        {
        }

        public virtual void Rollback()
        {
        }
    }
}