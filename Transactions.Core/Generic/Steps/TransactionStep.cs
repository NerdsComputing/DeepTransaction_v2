using System;

namespace Transactions.Core.Generic.Steps
{
    public abstract class TransactionStep<TInput, TOutput> : TransactionStep, IHasInput<TInput>, IHasOutput<TOutput>
    {
        public Type InputType { get; }

        public Type OutputType { get; }

        public abstract TOutput Output { get; }

        protected TransactionStep()
        {
            InputType = typeof(TInput);
            OutputType = typeof(TOutput);
        }

        public abstract void Initialize(TInput input);

        public void Initialize(object input)
        {
            Initialize((TInput) input);
        }

        public object GetOutput()
        {
            return Output;
        }
    }
}