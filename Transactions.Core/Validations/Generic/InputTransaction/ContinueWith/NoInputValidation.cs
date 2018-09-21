using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.InputTransaction.ContinueWith
{
    public class NoInputValidation : IValidation<ITransactionStep>
    {
        private readonly IEnumerable<ITransactionStep> _transactionSteps;

        public NoInputValidation(IEnumerable<ITransactionStep> transactionSteps)
        {
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            if (_transactionSteps.Any())
            {
                ITransactionStep lastStep = _transactionSteps.Last();

                if (lastStep is IHasOutput lastStepOutput && !(target is IHasInput))
                {
                    var message = "The input of the step you try to add differs from the last step output. Last step" +
                                  $"output: {lastStepOutput.OutputType}. Your step input: void.";

                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}