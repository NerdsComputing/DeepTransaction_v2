using System;

namespace Transactions.Core.Generic.Steps
{
    public abstract class OutputTransactionStep<TOutput> : TransactionStep, IHasOutput<TOutput>
    {
        public Type OutputType { get; }

        public abstract TOutput Output { get; }

        protected OutputTransactionStep()
        {
            OutputType = typeof(TOutput);
        }

        public object GetOutput()
        {
            return Output;
        }
    }
}