using System;
using System.Collections.Generic;
using Transactions.Core.Generic.Steps;
using Transactions.Core.Validations;
using Transactions.Core.Validations.Generic;
using Transactions.Core.Validations.Generic.OutputTransaction.Execute;
using Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith;
using ExecuteNoLastOutputValidation =
    Transactions.Core.Validations.Generic.OutputTransaction.Execute.NoLastOutputValidation;
using ContinueNoLastOutputValidation =
    Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith.NoLastOutputValidation;

namespace Transactions.Core.Generic.Transactions
{
    public class OutputTransaction<TOutput> : OutputTransactionStep<TOutput>
    {
        private readonly IList<ITransactionStep> _steps;

        private TOutput _output;

        protected virtual IEnumerable<IValidation<ITransactionStep>> ContinueValidations { get; }

        protected virtual IEnumerable<IValidation> ExecuteValidations { get; }

        public override TOutput Output => _output;

        public OutputTransaction()
        {
            _steps = new List<ITransactionStep>();
            ContinueValidations = BuildContinueValidations();
            ExecuteValidations = BuildExecuteValidations();
        }

        public OutputTransaction(TOutput output) : this()
        {
            _output = output;
        }

        public OutputTransaction<TOutput> ContinueWith(ITransactionStep step)
        {
            RunContinueValidations(step);
            _steps.Add(step);

            return this;
        }

        public sealed override void Execute()
        {
            RunExecuteValidations();
            BeforeExecution();
            object output = ExecuteSteps();
            AfterExecution();

            _output = (TOutput) output;
        }

        private IEnumerable<IValidation> BuildExecuteValidations()
        {
            return new IValidation[]
            {
                new ExecuteNoLastOutputValidation(_steps, this),
                new WrongLastOutputValidation(_steps, this),
            };
        }

        private IEnumerable<IValidation<ITransactionStep>> BuildContinueValidations()
        {
            return new IValidation<ITransactionStep>[]
            {
                new FirstInputValidation(_steps),
                new NoInputValidation(_steps),
                new ContinueNoLastOutputValidation(_steps),
                new InputValidation(_steps),
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

        private object ExecuteSteps()
        {
            try
            {
                var engine = new TransactionExecutionEngine(_steps, _output);
                engine.Execute();
                return engine.Current;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }
    }
}