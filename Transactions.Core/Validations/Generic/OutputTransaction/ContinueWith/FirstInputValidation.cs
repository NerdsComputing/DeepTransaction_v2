using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith
{
    public class FirstInputValidation : IValidation<ITransactionStep>
    {
        private readonly IEnumerable<ITransactionStep> _transactionSteps;

        public FirstInputValidation(IEnumerable<ITransactionStep> transactionSteps)
        {
            _transactionSteps = transactionSteps;
        }

        public void Validate(ITransactionStep target)
        {
            if (!_transactionSteps.Any())
            {
                if (target is IHasInput stepInput)
                {
                    var message = "The input of your step differs from the transaction input. Transaction input: " +
                                  $"void. Your step input: {stepInput.InputType}.";
                    
                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}