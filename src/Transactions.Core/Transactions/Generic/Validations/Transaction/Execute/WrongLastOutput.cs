using System;
using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Contract;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Transactions.Generic.Validations.Transaction.Execute
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

            if (last is IIOExecute || last is IOutputExecute)
            {
                IEnumerable<Type> arguments = last.GetType().GetGenericArguments();

                if (arguments.Last() != typeof(TRequiredOutput))
                {
                    throw new WrongSuccessionException();
                }
            }
        }
    }
}