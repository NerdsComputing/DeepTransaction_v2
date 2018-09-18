using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.InputTransaction.Execute
{
    public class LastOutputValidation : IValidation
    {
        private readonly IEnumerable<ITransactionStep> _steps;

        public LastOutputValidation(IEnumerable<ITransactionStep> transactionSteps)
        {
            _steps = transactionSteps;
        }

        public void Validate()
        {
            if (_steps.Any())
            {
                ITransactionStep lastStep = _steps.Last();

                if (lastStep is IHasOutput lastStepOutput)
                {
                    var message = "The last step output differs from the transaction output. Last step output: " +
                                  $"{lastStepOutput.OutputType}. Transaction output: void.";

                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}