using Transactions.Core.Contract.TransactionSteps.Generic;

namespace Transactions.Core.Transactions.Generic.Grammar.InputTransaction
{
    public class IOInputContinue<TOriginalInput, TRequiredInput> : ContinueBase<TOriginalInput>
    {
        public IOInputContinue(InputTransaction<TOriginalInput> transaction) : base(transaction)
        {
        }

        public OutputVoidContinueExecute<TOriginalInput> ContinueWith(IInputTransactionStep<TRequiredInput> step)
        {
            Transaction.Entities.Add(step);
            return new OutputVoidContinueExecute<TOriginalInput>(Transaction);
        }

        public IOInputContinue<TOriginalInput, TOutput>
            ContinueWith<TOutput>(ITransactionStep<TRequiredInput, TOutput> step)
        {
            Transaction.Entities.Add(step);
            return new IOInputContinue<TOriginalInput, TOutput>(Transaction);
        }
    }
}