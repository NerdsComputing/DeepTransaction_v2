using System;
using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;
using Transactions.Core.Generic.Transactions;

namespace Transactions.Core
{
    public class Transaction : TransactionStep
    {
        private readonly IList<ITransactionStep> _steps;

        public Transaction()
        {
            _steps = new List<ITransactionStep>();
        }

        public static Transaction Create()
        {
            return new Transaction();
        }

        public static Transaction<TInput, TOutput> Create<TInput, TOutput>()
        {
            return new Transaction<TInput, TOutput>();
        }

        public static Transaction<TInput, TOutput> Create<TInput, TOutput>(TInput input)
        {
            return new Transaction<TInput, TOutput>(input);
        }

        public Transaction ContinueWith(ITransactionStep step)
        {
            if (!_steps.Any())
            {
                ValidateFirstStep(step);
            }
            else
            {
                ValidateStep(step);
            }

            _steps.Add(step);

            return this;
        }

        private void ValidateStep(ITransactionStep step)
        {
            if (step is IHasInput stepInput)
            {
                var message = "The input of the step you try to add does not match with the output of the last " +
                              "existent step. Last step output: {0}. Your step input: {1}.";

                ITransactionStep lastStep = _steps.Last();
                if (lastStep is IHasOutput lastStepOutput)
                {
                    if (lastStepOutput.OutputType != stepInput.InputType)
                    {
                        var formattedMessage = string.Format(message, lastStepOutput.OutputType, stepInput.InputType);
                        throw new WrongSuccessionException(formattedMessage);
                    }
                }
                else
                {
                    var formattedMessage = string.Format(message, "void", stepInput.InputType);
                    throw new WrongSuccessionException(formattedMessage);
                }
            }
        }

        private static void ValidateFirstStep(ITransactionStep step)
        {
            if (step is IHasInput hasInput)
            {
                var message = $"The input of the first step you try to add does not match with the input of the " +
                              $"transaction. Step input: {hasInput.InputType}. Transaction input: void.";

                throw new WrongSuccessionException(message);
            }
        }

        public override void Execute()
        {
            BeforeExecution();
            ExecuteSteps();
            AfterExecution();
        }

        private void ExecuteSteps()
        {
            try
            {
                var engine = new TransactionExecutionEngine(_steps);
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