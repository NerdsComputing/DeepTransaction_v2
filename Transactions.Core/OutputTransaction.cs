using Transactions.Core.Generic.Transactions;

namespace Transactions.Core
{
    public static class OutputTransaction
    {
        public static OutputTransaction<TOutput> Create<TOutput>()
        {
            return new OutputTransaction<TOutput>();
        }

        public static OutputTransaction<TOutput> Create<TOutput>(TOutput output)
        {
            return new OutputTransaction<TOutput>(output);
        }
    }
}