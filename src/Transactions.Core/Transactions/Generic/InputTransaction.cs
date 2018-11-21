using Transactions.Core.Contract.Transactions.Generic;
using Transactions.Core.Contract.TransactionSteps.Generic;
using Transactions.Core.Transactions.Generic.Grammar.InputTransaction;
using Transactions.Core.Transactions.Generic.Validations;
using Transactions.Core.Transactions.Generic.Validations.InputTransaction.Execute;

namespace Transactions.Core.Transactions.Generic
{
    public class InputTransaction<TInput> : TransactionBase, IInputTransaction<TInput>
    {
        private protected override IEngine Engine { get; }

        public InputTransaction()
        {
            Engine = new Engine(Entities);
        }

        public OutputVoidContinueExecute<TInput> ContinueWith(IInputTransactionStep<TInput> step)
        {
            Entities.Add(step);
            return new OutputVoidContinueExecute<TInput>(this);
        }

        public IOInputContinue<TInput, TStepOutput> ContinueWith<TStepOutput>(
            ITransactionStep<TInput, TStepOutput> step)
        {
            Entities.Add(step);
            return new IOInputContinue<TInput, TStepOutput>(this);
        }

        public void Execute(TInput input)
        {
            RunExecuteValidations();
            Engine.Current = input;
            BaseExecute();
        }

        public void Execute(object input)
        {
            Execute((TInput) input);
        }

        private void RunExecuteValidations()
        {
            var validations = new IValidation[]
            {
                new EmptyQueue(Entities),
                new WrongLastStep(Entities), 
            };

            foreach (IValidation validation in validations)
            {
                validation.Validate();
            }
        }
    }
}