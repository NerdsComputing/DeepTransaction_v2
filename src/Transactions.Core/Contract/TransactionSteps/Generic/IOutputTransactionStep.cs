using Transactions.Core.Contract.Generic;

namespace Transactions.Core.Contract.TransactionSteps.Generic
{
    public interface IOutputTransactionStep<out TOutput> :
        ITransactionEntity, IBeforeExecution, IOutputExecute<TOutput>, IAfterExecution, IRollback
    {
    }
}