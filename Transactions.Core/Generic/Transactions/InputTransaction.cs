using System;
using System.Collections.Generic;
using Transactions.Core.Generic.Steps;
using Transactions.Core.Validations;
using Transactions.Core.Validations.Generic;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Transactions.Core.Validations.Generic.InputTransaction.Execute;

namespace Transactions.Core.Generic.Transactions
{
    public class InputTransaction<TInput> : InputTransactionStep<TInput>
    {
        private readonly IList<ITransactionStep> _steps;

        private TInput _input;

        protected virtual IEnumerable<IValidation<ITransactionStep>> ContinueValidations { get; }

        protected virtual IEnumerable<IValidation> ExecuteValidations { get; }

        public InputTransaction()
        {
            _steps = new List<ITransactionStep>();
            ContinueValidations = BuildContinueValidations();
            ExecuteValidations = BuildExecuteValidations();
        }

        public InputTransaction(TInput input) : this()
        {
            Initialize(input);
        }

        public sealed override void Initialize(TInput input)
        {
            _input = input;
        }

        public InputTransaction<TInput> ContinueWith(ITransactionStep step)
        {
            RunContinueValidations(step);
            _steps.Add(step);

            return this;
        }

        public sealed override void Execute()
        {
            RunExecuteValidations();
            BeforeExecution();
            ExecuteSteps();
            AfterExecution();
        }

        private IEnumerable<IValidation> BuildExecuteValidations()
        {
            return new IValidation[]
            {
                new LastOutputValidation(_steps),
            };
        }

        private IEnumerable<IValidation<ITransactionStep>> BuildContinueValidations()
        {
            return new IValidation<ITransactionStep>[]
            {
                new NoFirstInputValidation(_steps, this),
                new WrongFirstInputValidation(_steps, this),
                new WrongInputValidation(_steps),
                new NoInputValidation(_steps),
                new NoLastOutputValidation(_steps),
            };
        }

        private void RunContinueValidations(ITransactionStep step)
        {
            foreach (IValidation<ITransactionStep> validation in ContinueValidations)
            {
                validation.Validate(step);
            }
        }

        private void RunExecuteValidations()
        {
            foreach (IValidation validation in ExecuteValidations)
            {
                validation.Validate();
            }
        }

        private void ExecuteSteps()
        {
            try
            {
                var engine = new TransactionExecutionEngine(_steps, _input);
                engine.Execute();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }
    }
}