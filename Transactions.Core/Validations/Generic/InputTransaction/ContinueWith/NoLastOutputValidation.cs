using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.InputTransaction.ContinueWith
{
    public class NoLastOutputValidation : IValidation<ITransactionStep>
    {
        private readonly IEnumerable<ITransactionStep> _transactionSteps;

        public NoLastOutputValidation(IEnumerable<ITransactionStep> transactionSteps)
        {
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            if (_transactionSteps.Any())
            {
                ITransactionStep lastStep = _transactionSteps.Last();

                if (target is IHasInput stepInput && !(lastStep is IHasOutput))
                {
                    var message = "The input of the step you try to add differs from the last step output. Last " +
                                  $"existent step output: void. Your step input: {stepInput.InputType}.";

                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}