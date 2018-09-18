using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith
{
    public class InputValidation : IValidation<ITransactionStep>
    {
        private readonly IEnumerable<ITransactionStep> _transactionSteps;

        public InputValidation(IEnumerable<ITransactionStep> transactionSteps)
        {
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            if (_transactionSteps.Any())
            {
                ITransactionStep lastStep = _transactionSteps.Last();

                if (lastStep is IHasOutput lastStepOutput && target is IHasInput stepInput)
                {
                    if (lastStepOutput.OutputType != stepInput.InputType)
                    {
                        var message = "The input of the step you try to add differs from the last step output. " +
                                      $"Last step output is: {lastStepOutput.OutputType}." +
                                      $"Your step input is: {stepInput.InputType}.";

                        throw new WrongSuccessionException(message);
                    }
                }
            }
        }
    }
}