using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.TransactionSteps.Generic
{
    public abstract class InputTransactionStep<TInput> : IInputTransactionStep<TInput>
    {
        public virtual void BeforeExecution()
        {
        }

        public void Execute(object input)
        {
            Execute((TInput) input);
        }

        public virtual void AfterExecution()
        {
        }

        public virtual void Rollback()
        {
        }

        public abstract void Execute(TInput input);
    }
}