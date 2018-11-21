namespace Transactions.Core.Transactions.Generic.Grammar.Transaction
{
    public class IOInputContinueExecute<TOriginalInput, TOriginalOutput, TRequiredInput> :
        IOInputContinue<TOriginalInput, TOriginalOutput, TRequiredInput>
    {
        public IOInputContinueExecute(Transaction<TOriginalInput, TOriginalOutput> transaction) : base(transaction)
        {
        }

        public TOriginalOutput Execute(TOriginalInput input)
        {
            return Transaction.Execute(input);
        }
    }
}