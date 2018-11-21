using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Contract;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Transactions.Validations.Transaction.Execute
{
    public class LastInputOutput : IValidation
    {
        private readonly IEnumerable<ITransactionEntity> _entities;

        public LastInputOutput(IEnumerable<ITransactionEntity> entities)
        {
            _entities = entities;
        }
        
        public void Validate()
        {
            ITransactionEntity last = _entities.Last();

            if (last is IIOExecute)
            {
                throw new WrongSuccessionException();
            }
        }
    }
}