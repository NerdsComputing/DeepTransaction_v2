namespace Transactions.Core.Transactions.Generic.Grammar.OutputTransaction
{
    public class IOInputContinueExecute<TOriginalOutput, TRequiredInput> :
        IOInputContinue<TOriginalOutput, TRequiredInput>
    {
        public IOInputContinueExecute(OutputTransaction<TOriginalOutput> transaction) : base(transaction)
        {
        }

        public TOriginalOutput Execute()
        {
            return (TOriginalOutput) Transaction.Execute();
        }
    }
}