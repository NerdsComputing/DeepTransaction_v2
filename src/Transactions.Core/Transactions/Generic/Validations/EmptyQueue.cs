using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Contract;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Transactions.Generic.Validations
{
    public class EmptyQueue : IValidation
    {
        private readonly IEnumerable<ITransactionEntity> _entities;

        public EmptyQueue(IEnumerable<ITransactionEntity> entities)
        {
            _entities = entities;
        }

        public void Validate()
        {
            if (!_entities.Any())
            {
                throw new WrongSuccessionException("The transaction you try to execute contains no steps.");
            }
        }
    }
}