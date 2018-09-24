using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Contract.Transactions.Generic
{
    public interface IInputTransaction<in TInput> : IInputTransactionStep<TInput>
    {
    }
}