using Transactions.Core.Contract.TransactionSteps;
using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Transactions.Generic.Grammar.OutputTransaction
{
    public class OutputVoidContinue<TOriginalOutput> : ContinueBase<TOriginalOutput>
    {
        public OutputVoidContinue(OutputTransaction<TOriginalOutput> transaction) : base(transaction)
        {
        }

        public OutputVoidContinueExecute<TOriginalOutput> ContinueWith(IOutputTransactionStep<TOriginalOutput> step)
        {
            Transaction.Entities.Add(step);
            return new OutputVoidContinueExecute<TOriginalOutput>(Transaction);
        }

        public IOInputContinue<TOriginalOutput, TOutput> ContinueWith<TOutput>(IOutputTransactionStep<TOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinue<TOriginalOutput, TOutput>(Transaction);
        }

        public OutputVoidContinue<TOriginalOutput> ContinueWith(ITransactionStep step)
        {
            Transaction.Entities.Add(step);
            return this;
        }
    }
}