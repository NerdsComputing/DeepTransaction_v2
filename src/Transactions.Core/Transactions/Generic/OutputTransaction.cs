using Transactions.Core.Contract.Generic;
using Transactions.Core.Contract.Transactions.Generic;
using Transactions.Core.Contract.TransactionSteps;
using Transactions.Core.Contract.TransactionSteps.Generic;
using Transactions.Core.Transactions.Generic.Grammar.OutputTransaction;
using Transactions.Core.Transactions.Generic.Validations;
using Transactions.Core.Transactions.Generic.Validations.OutputTransaction.Execute;

namespace Transactions.Core.Transactions.Generic
{
    public class OutputTransaction<TOutput> : TransactionBase, IOutputTransaction<TOutput>
    {
        private protected override IEngine Engine { get; }

        public OutputTransaction()
        {
            Engine = new Engine(Entities);
        }

        public OutputVoidContinue<TOutput> ContinueWith(ITransactionStep step)
        {
            Entities.Add(step);
            return new OutputVoidContinue<TOutput>(this);
        }

        public IOInputContinueExecute<TOutput, TOutput> ContinueWith(IOutputTransactionStep<TOutput> step)
        {
            Entities.Add(step);
            return new IOInputContinueExecute<TOutput, TOutput>(this);
        }

        public IOInputContinue<TOutput, TStepOutput> ContinueWith<TStepOutput>(IOutputTransactionStep<TStepOutput> step)
        {
            Entities.Add(step);
            return new IOInputContinue<TOutput, TStepOutput>(this);
        }

        TOutput IOutputExecute<TOutput>.Execute()
        {
            RunExecuteValidations();
            BaseExecute();
            return (TOutput) Engine.Current;
        }

        public object Execute()
        {
            return ((IOutputExecute<TOutput>) this).Execute();
        }

        private void RunExecuteValidations()
        {
            var validations = new IValidation[]
            {
                new EmptyQueue(Entities),
                new WrongLastOutput<TOutput>(Entities),
            };

            foreach (IValidation validation in validations)
            {
                validation.Validate();
            }
        }
    }
}