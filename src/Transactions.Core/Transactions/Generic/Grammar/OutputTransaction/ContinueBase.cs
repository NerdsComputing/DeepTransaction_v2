namespace Transactions.Core.Transactions.Generic.Grammar.OutputTransaction
{
    public abstract class ContinueBase<TOriginalOutput>
    {
        protected readonly OutputTransaction<TOriginalOutput> Transaction;

        protected ContinueBase(OutputTransaction<TOriginalOutput> transaction)
        {
            Transaction = transaction;
        }
    }
}