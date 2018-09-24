namespace Transactions.Core.Transactions.Generic.Grammar.Transaction
{
    public abstract class ContinueBase<TOriginalInput, TOriginalOutput>
    {
        protected readonly Transaction<TOriginalInput, TOriginalOutput> Transaction;

        protected ContinueBase(Transaction<TOriginalInput, TOriginalOutput> transaction)
        {
            Transaction = transaction;
        }
    }
}