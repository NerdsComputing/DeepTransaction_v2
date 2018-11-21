using System;
using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Contract;
using Transactions.Core.Contract.Generic;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Transactions.Generic.Validations.OutputTransaction.Execute
{
    public class WrongLastOutput<TRequiredOutput> : IValidation
    {
        private readonly IEnumerable<ITransactionEntity> _entities;

        public WrongLastOutput(IEnumerable<ITransactionEntity> entities)
        {
            _entities = entities;
        }

        public void Validate()
        {
            ITransactionEntity last = _entities.Last();

            if (!(last is IOutputExecute<TRequiredOutput>) && !HasRightOutput(last))
            {
                throw new WrongSuccessionException();
            }
        }

        private bool HasRightOutput(ITransactionEntity entity)
        {
            if (entity is IIOExecute)
            {
                IEnumerable<Type> arguments = entity.GetType().GetGenericArguments();

                if (arguments.Last() == typeof(TRequiredOutput))
                {
                    return true;
                }
            }

            return false;
        }
    }
}