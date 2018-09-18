using System;
using System.Collections.Generic;

namespace Transactions.Core
{
    public class TransactionExecutionEngine
    {
        private readonly IList<ITransactionStep> _transactionSteps;

        public object Current { get; private set; }

        public TransactionExecutionEngine(IList<ITransactionStep> transactionSteps, object current = null)
        {
            _transactionSteps = transactionSteps;
            Current = current;
        }

        public void Execute()
        {
            foreach (ITransactionStep step in _transactionSteps)
            {
                try
                {
                    ExecuteStep(step);
                }
                catch (Exception)
                {
                    RollbackExecutedSteps(step);
                    throw;
                }
            }
        }

        private void RollbackExecutedSteps(ITransactionStep failed)
        {
            int failedIndex = _transactionSteps.IndexOf(failed);

            for (int index = failedIndex; index >= 0; index--)
            {
                ITransactionStep foundStep = _transactionSteps[index];
                foundStep.Rollback();
            }
        }

        private void ExecuteStep(ITransactionStep step)
        {
            var hasInput = step as IHasInput;
            hasInput?.Initialize(Current);

            step.BeforeExecution();
            step.Execute();
            step.AfterExecution();

            var hasOutput = step as IHasOutput;
            Current = hasOutput?.GetOutput();
        }
    }
}