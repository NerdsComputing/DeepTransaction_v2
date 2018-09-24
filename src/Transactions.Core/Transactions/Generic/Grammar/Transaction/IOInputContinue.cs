using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Transactions.Generic.Grammar.Transaction
{
    public class IOInputContinue<TOriginalInput, TOriginalOutput, TRequiredInput> :
        ContinueBase<TOriginalInput, TOriginalOutput>
    {
        public IOInputContinue(Transaction<TOriginalInput, TOriginalOutput> transaction) : base(transaction)
        {
        }

        public OutputVoidContinue<TOriginalInput, TOriginalOutput>
            ContinueWith(IInputTransactionStep<TRequiredInput> step)
        {
            Transaction.Entities.Add(step);
            return new OutputVoidContinue<TOriginalInput, TOriginalOutput>(Transaction);
        }

        public IOInputContinueExecute<TOriginalInput, TOriginalOutput, TOriginalInput>
            ContinueWith(ITransactionStep<TRequiredInput, TOriginalOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinueExecute<TOriginalInput, TOriginalOutput, TOriginalInput>(Transaction);
        }

        public IOInputContinue<TOriginalInput, TOriginalOutput, TStepOutput>
            ContinueWith<TStepOutput>(ITransactionStep<TRequiredInput, TStepOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinue<TOriginalInput, TOriginalOutput, TStepOutput>(Transaction);
        }
    }
}