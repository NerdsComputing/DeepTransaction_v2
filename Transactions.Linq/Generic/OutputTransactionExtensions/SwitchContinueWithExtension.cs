using Transactions.Core;
using Transactions.Core.Generic.Transactions;

namespace Transactions.Linq.Generic.OutputTransactionExtensions
{
    public static class SwitchContinueWithExtension
    {
        public static Transaction<TTargetInput, TTargetOutput> SwitchContinueWith<TOutput, TTargetInput, TTargetOutput>(
            this OutputTransaction<TOutput> instance,
            Transaction<TTargetInput, TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static InputTransaction<TTargetInput> SwitchContinueWith<TOutput, TTargetInput>(
            this OutputTransaction<TOutput> instance,
            InputTransaction<TTargetInput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static OutputTransaction<TTargetOutput> SwitchContinueWith<TOutput, TTargetOutput>(
            this OutputTransaction<TOutput> instance,
            OutputTransaction<TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static Transaction SwitchContinueWith<TOutput>(
            this OutputTransaction<TOutput> instance,
            Transaction transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }
    }
}