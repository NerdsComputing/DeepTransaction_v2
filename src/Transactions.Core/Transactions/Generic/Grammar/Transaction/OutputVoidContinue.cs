using Transactions.Core.Contract.TransactionSteps;
using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Transactions.Generic.Grammar.Transaction
{
    public class OutputVoidContinue<TOriginalInput, TOriginalOutput> :
        ContinueBase<TOriginalInput, TOriginalOutput>
    {
        public OutputVoidContinue(Transaction<TOriginalInput, TOriginalOutput> transaction) : base(transaction)
        {
        }

        public IOInputContinueExecute<TOriginalInput, TOriginalOutput, TOriginalOutput>
            ContinueWith(IOutputTransactionStep<TOriginalOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinueExecute<TOriginalInput, TOriginalOutput, TOriginalOutput>(Transaction);
        }

        public IOInputContinue<TOriginalInput, TOriginalOutput, TStepOutput>
            ContinueWith<TStepOutput>(IOutputTransactionStep<TStepOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinue<TOriginalInput, TOriginalOutput, TStepOutput>(Transaction);
        }

        public OutputVoidContinue<TOriginalInput, TOriginalOutput> ContinueWith(ITransactionStep step)
        {
            Transaction.Entities.Add(step);
            return this;
        }
    }
}