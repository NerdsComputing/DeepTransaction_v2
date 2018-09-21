namespace Transactions.Core
{
    public interface ITransactionStep
    {
        void BeforeExecution();
        void Execute();
        void AfterExecution();
        void Rollback();
    }
}