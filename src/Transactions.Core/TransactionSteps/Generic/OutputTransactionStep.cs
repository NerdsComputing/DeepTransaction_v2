using Transactions.Core.Contract;
using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.TransactionSteps.Generic
{
    public abstract class OutputTransactionStep<TOutput> : IOutputTransactionStep<TOutput>
    {
        public virtual void BeforeExecution()
        {
        }

        object IOutputExecute.Execute()
        {
            return Execute();
        }

        public virtual void AfterExecution()
        {
        }

        public virtual void Rollback()
        {
        }

        public abstract TOutput Execute();
    }
}