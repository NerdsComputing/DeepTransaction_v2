using System;
using System.Collections.Generic;
using Transactions.Core.Generic.Steps;
using Transactions.Core.Validations.Generic;

namespace Transactions.Core.Generic.Transactions
{
    public class Transaction<TInput, TOutput> : TransactionStep<TInput, TOutput>
    {
        private readonly IList<ITransactionStep> _steps;

        private TInput _input;
        private TOutput _output;

        protected virtual IEnumerable<IValidation<ITransactionStep>> ContinueValidation { get; }

        public override TOutput Output => _output;

        public Transaction()
        {
            _steps = new List<ITransactionStep>();
            ContinueValidation = BuildContinueValidation();
        }

        public Transaction(TInput input) : this()
        {
            Initialize(input);
        }

        public Transaction<TInput, TOutput> ContinueWith(ITransactionStep step)
        {
            _steps.Add(step);

            return this;
        }

        public sealed override void Initialize(TInput input)
        {
            _input = input;
        }

        public sealed override void Execute()
        {
            BeforeExecution();
            object output = ExecuteSteps();
            AfterExecution();

            _output = (TOutput) output;
        }

        private static IEnumerable<IValidation<ITransactionStep>> BuildContinueValidation()
        {
            return new IValidation<ITransactionStep>[]
            {
                
            };
        }

        private object ExecuteSteps()
        {
            try
            {
                var engine = new TransactionExecutionEngine(_steps, _input);
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