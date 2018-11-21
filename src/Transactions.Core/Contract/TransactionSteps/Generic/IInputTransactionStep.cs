using Transactions.Core.Contract.Generic;

namespace Transactions.Core.Contract.TransactionSteps.Generic
{
    public interface IInputTransactionStep<in TInput> :
        ITransactionEntity, IBeforeExecution, IInputExecute<TInput>, IAfterExecution, IRollback
    {
    }
}