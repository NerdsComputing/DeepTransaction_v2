using System;

namespace Transactions.Core.Generic.Steps
{
    public abstract class InputTransactionStep<TInput> : TransactionStep, IHasInput<TInput>
    {
        public Type InputType { get; }

        protected InputTransactionStep()
        {
            InputType = typeof(TInput);
        }

        public abstract void Initialize(TInput input);

        public void Initialize(object input)
        {
            Initialize((TInput) input);
        }
    }
}