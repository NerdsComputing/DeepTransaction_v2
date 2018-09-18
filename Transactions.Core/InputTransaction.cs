using Transactions.Core.Generic.Transactions;

namespace Transactions.Core
{
    public static class InputTransaction
    {
        public static InputTransaction<TInput> Create<TInput>()
        {
            return new InputTransaction<TInput>();
        }

        public static InputTransaction<TInput> Create<TInput>(TInput input)
        {
            return new InputTransaction<TInput>(input);
        }
    }
}