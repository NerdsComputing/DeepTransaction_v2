using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Transactions.Grammar.Transaction
{
    public class IOInputContinue<TRequiredInput> : ContinueBase
    {
        public IOInputContinue(Transactions.Transaction transaction) : base(transaction)
        {
        }

        public IOInputContinue<TStepOutput> ContinueWith<TStepOutput>(
            ITransactionStep<TRequiredInput, TStepOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinue<TStepOutput>(Transaction);
        }

        public OutputVoidContinueExecute ContinueWith(IInputTransactionStep<TRequiredInput> step)
        {
            Transaction.Entities.Add(step);
            return new OutputVoidContinueExecute(Transaction);
        }
    }
}