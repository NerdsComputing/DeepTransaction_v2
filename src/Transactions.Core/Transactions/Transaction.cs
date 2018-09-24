using Transactions.Core.Contract.Transactions;
using Transactions.Core.Contract.TransactionSteps;
using Transactions.Core.Contract.TransactionSteps.Generic;
using Transactions.Core.Transactions.Generic;
using Transactions.Core.Transactions.Generic.Validations;
using Transactions.Core.Transactions.Grammar.Transaction;
using Transactions.Core.Transactions.Validations.Transaction.Execute;

namespace Transactions.Core.Transactions
{
    public class Transaction : TransactionBase, ITransaction
    {
        private protected override IEngine Engine { get; }

        public Transaction()
        {
            Engine = new Engine(Entities);
        }

        public static Transaction Create()
        {
            return new Transaction();
        }

        public static Transaction<TInput, TOutput> Create<TInput, TOutput>()
        {
            return new Transaction<TInput, TOutput>();
        }

        public OutputVoidContinueExecute ContinueWith(ITransactionStep step)
        {
            Entities.Add(step);
            return new OutputVoidContinueExecute(this);
        }

        public IOInputContinue<TStepOutput> ContinueWith<TStepOutput>(IOutputTransactionStep<TStepOutput> step)
        {
            Entities.Add(step);
            return new IOInputContinue<TStepOutput>(this);
        }

        public void Execute()
        {
            RunExecuteValidations();
            BaseExecute();
        }

        private void RunExecuteValidations()
        {
            var validations = new IValidation[]
            {
                new EmptyQueue(Entities),
                new LastOutput(Entities),
                new LastInputOutput(Entities),
            };

            foreach (IValidation validation in validations)
            {
                validation.Validate();
            }
        }
    }
}