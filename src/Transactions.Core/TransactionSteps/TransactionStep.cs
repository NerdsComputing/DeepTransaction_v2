using Transactions.Core.Contract.TransactionSteps;

namespace Transactions.Core.TransactionSteps
{
    public abstract class TransactionStep : ITransactionStep
    {
        public virtual void BeforeExecution()
        {
        }

        public virtual void AfterExecution()
        {
        }

        public virtual void Rollback()
        {
        }

        public abstract void Execute();
    }
}