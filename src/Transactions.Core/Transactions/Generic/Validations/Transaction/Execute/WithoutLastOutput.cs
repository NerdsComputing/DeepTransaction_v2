using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Contract;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Transactions.Generic.Validations.Transaction.Execute
{
    public class WithoutLastOutput : IValidation
    {
        private readonly IEnumerable<ITransactionEntity> _entities;

        public WithoutLastOutput(IEnumerable<ITransactionEntity> entities)
        {
            _entities = entities;
        }
        
        public void Validate()
        {
            ITransactionEntity last = _entities.Last();

            if (last is IExecute || last is IInputExecute)
            {
                throw new WrongSuccessionException();
            }
        }
    }
}