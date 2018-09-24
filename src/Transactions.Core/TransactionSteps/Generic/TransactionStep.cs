using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.TransactionSteps.Generic
{
    public abstract class TransactionStep<TInput, TOutput> : ITransactionStep<TInput, TOutput>
    {
        public virtual void BeforeExecution()
        {
        }

        public object Execute(object input)
        {
            return Execute((TInput) input);
        }

        public virtual void AfterExecution()
        {
        }

        public virtual void Rollback()
        {
        }

        public abstract TOutput Execute(TInput input);
    }
}