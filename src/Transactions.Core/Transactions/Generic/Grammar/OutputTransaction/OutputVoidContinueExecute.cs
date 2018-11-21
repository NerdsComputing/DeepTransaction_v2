namespace Transactions.Core.Transactions.Generic.Grammar.OutputTransaction
{
    public class OutputVoidContinueExecute<TOriginalOutput> : OutputVoidContinue<TOriginalOutput>
    {
        public OutputVoidContinueExecute(OutputTransaction<TOriginalOutput> transaction) : base(transaction)
        {
        }

        public TOriginalOutput Execute()
        {
            return (TOriginalOutput) Transaction.Execute();
        }
    }
}