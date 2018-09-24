using Transactions.Core.Contract.Generic;

namespace Transactions.Core.Contract.TransactionSteps.Generic
{
    public interface ITransactionStep<in TInput, out TOutput> :
        ITransactionEntity, IBeforeExecution, IExecute<TInput, TOutput>, IAfterExecution, IRollback
    {
    }
}