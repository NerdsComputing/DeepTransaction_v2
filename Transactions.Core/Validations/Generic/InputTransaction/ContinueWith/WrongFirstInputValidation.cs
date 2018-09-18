using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.InputTransaction.ContinueWith
{
    public class WrongFirstInputValidation : IValidation<ITransactionStep>
    {
        private readonly IHasInput _transactionInput;
        private readonly IEnumerable<ITransactionStep> _transactionSteps;

        public WrongFirstInputValidation(IEnumerable<ITransactionStep> transactionSteps, IHasInput transactionInput)
        {
            _transactionInput = transactionInput;
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            const string messageTemplate = "The input of the step you try to add differs from the input of the " +
                                           "transaction. Transaction input: {0}. Your step input: {1}";

            if (!_transactionSteps.Any() && target is IHasInput stepInput)
            {
                if (_transactionInput.InputType != stepInput.InputType)
                {
                    var message = string.Format(messageTemplate, _transactionInput.InputType, stepInput.InputType);
                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}