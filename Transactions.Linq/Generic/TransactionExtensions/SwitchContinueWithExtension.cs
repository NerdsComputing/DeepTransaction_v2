using Transactions.Core;
using Transactions.Core.Generic.Transactions;

namespace Transactions.Linq.Generic.TransactionExtensions
{
    public static class SwitchContinueWithExtension
    {
        public static Transaction<TTargetInput, TTargetOutput> SwitchContinueWith<TInput, TOutput, TTargetInput,
            TTargetOutput>(this Transaction<TInput, TOutput> instance,
            Transaction<TTargetInput, TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static InputTransaction<TTargetInput> SwitchContinueWith<TInput, TOutput, TTargetInput>(
            this Transaction<TInput, TOutput> instance,
            InputTransaction<TTargetInput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static OutputTransaction<TTargetOutput> SwitchContinueWith<TInput, TOutput, TTargetOutput>(
            this Transaction<TInput, TOutput> instance,
            OutputTransaction<TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static Transaction SwitchContinueWith<TInput, TOutput>(
            this Transaction<TInput, TOutput> instance,
            Transaction transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }
    }
}