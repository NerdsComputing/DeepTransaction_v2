using Transactions.Core;
using Transactions.Core.Generic.Transactions;

namespace Transactions.Linq.TransactionExtensions
{
    public static class SwitchContinueWithExtension
    {
        public static Transaction SwitchContinueWith(this Transaction instance, Transaction transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static InputTransaction<TTargetInput> SwitchContinueWith<TTargetInput>(
            this Transaction instance,
            InputTransaction<TTargetInput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static OutputTransaction<TTargetInput> SwitchContinueWith<TTargetInput>(
            this Transaction instance,
            OutputTransaction<TTargetInput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }

        public static Transaction<TTargetInput, TTargetOutput> SwitchContinueWith<TTargetInput, TTargetOutput>(
            this Transaction instance,
            Transaction<TTargetInput, TTargetOutput> transaction)
        {
            instance.ContinueWith(transaction);

            return transaction;
        }
    }
}