namespace Transactions.Core.Transactions.Generic.Grammar.InputTransaction
{
    public abstract class ContinueBase<TOriginalInput>
    {
        protected readonly InputTransaction<TOriginalInput> Transaction;

        protected ContinueBase(InputTransaction<TOriginalInput> transaction)
        {
            Transaction = transaction;
        }
    }
}