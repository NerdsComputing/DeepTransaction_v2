using Transactions.Core;
using Transactions.Core.Generic.Transactions;

namespace Transactions.Linq.Generic.InputTransactionExtensions
{
    public static class SwitchContinueWithExtension
    {
        public static Transaction<TTargetInput, TTargetOutput> SwitchContinueWith<TInput, TTargetInput, TTargetOutput>(
            this InputTransaction<TInput> instance,
            Transaction<TTargetInput, TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static InputTransaction<TTargetInput> SwitchContinueWith<TInput, TTargetInput>(
            this InputTransaction<TInput> instance,
            InputTransaction<TTargetInput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static OutputTransaction<TTargetOutput> SwitchContinueWith<TInput, TTargetOutput>(
            this InputTransaction<TInput> instance,
            OutputTransaction<TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static Transaction SwitchContinueWith<TInput>(
            this InputTransaction<TInput> instance,
            Transaction transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }
    }
}