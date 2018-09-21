using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.InputTransaction.ContinueWith
{
    public class NoFirstInputValidation : IValidation<ITransactionStep>
    {
        private readonly IEnumerable<ITransactionStep> _transactionSteps;
        private readonly IHasInput _transactionInput;

        public NoFirstInputValidation(IEnumerable<ITransactionStep> transactionSteps, IHasInput transactionInput)
        {
            _transactionInput = transactionInput;
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            if (!_transactionSteps.Any() && !(target is IHasInput))
            {
                var message = "The input of the step you try to add differs from the transaction input." +
                              $"Transaction input: {_transactionInput.InputType}. Your step input: void.";

                throw new WrongSuccessionException(message);
            }
        }
    }
}