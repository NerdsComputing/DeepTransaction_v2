using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Contract.Transactions.Generic
{
    public interface IOutputTransaction<out TOutput> : IOutputTransactionStep<TOutput>
    {
    }
}