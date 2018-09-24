namespace Transactions.Core.Contract.TransactionSteps
{
    public interface ITransactionStep : ITransactionEntity, IBeforeExecution, IExecute, IAfterExecution, IRollback
    {
    }
}