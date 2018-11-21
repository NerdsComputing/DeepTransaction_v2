using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Contract.Transactions.Generic
{
    public interface ITransaction<in TInput, out TOutput> : ITransactionStep<TInput, TOutput>
    {
    }
}