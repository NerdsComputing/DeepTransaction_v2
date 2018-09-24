using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Transactions.Generic.Grammar.OutputTransaction
{
    public class IOInputContinue<TOriginalOutput, TRequiredInput> : ContinueBase<TOriginalOutput>
    {
        public IOInputContinue(OutputTransaction<TOriginalOutput> transaction) : base(transaction)
        {
        }

        public IOInputContinueExecute<TOriginalOutput, TRequiredInput>
            ContinueWith(ITransactionStep<TRequiredInput, TOriginalOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinueExecute<TOriginalOutput, TRequiredInput>(Transaction);
        }

        public IOInputContinue<TOriginalOutput, TOutput>
            ContinueWith<TOutput>(ITransactionStep<TRequiredInput, TOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinue<TOriginalOutput, TOutput>(Transaction);
        }

        public OutputVoidContinue<TOriginalOutput> ContinueWith(IInputTransactionStep<TRequiredInput> step)
        {
            Transaction.Entities.Add(step);
            return new OutputVoidContinue<TOriginalOutput>(Transaction);
        }
    }
}