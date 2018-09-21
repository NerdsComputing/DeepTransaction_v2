using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.InputTransaction.ContinueWith
{
    public class WrongInputValidation : IValidation<ITransactionStep>
    {
        private readonly IEnumerable<ITransactionStep> _transactionSteps;

        public WrongInputValidation(IEnumerable<ITransactionStep> transactionSteps)
        {
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            if (!_transactionSteps.Any())
            {
                return;
            }

            if (target is IHasInput stepInput && _transactionSteps.Last() is IHasOutput lastStepOutput)
            {
                if (lastStepOutput.OutputType != stepInput.InputType)
                {
                    var message = "The input of the step you try to add does not match with the output of " +
                                  $"the last existent step. Last existent step output: {lastStepOutput.OutputType}. " +
                                  $"Your step input: {stepInput.InputType}.";

                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}