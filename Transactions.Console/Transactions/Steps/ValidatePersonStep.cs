using System;
using Transactions.Console.Models;
using Transactions.Core.Generic.Steps;

namespace Transactions.Console.Transactions.Steps
{
    public class ValidatePersonStep : TransactionStep<Person, Person>
    {
        private Person _receivedPerson;

        public override Person Output => _receivedPerson;

        public override void Initialize(Person input)
        {
            _receivedPerson = input;
        }

        public override void Execute()
        {
            if (string.IsNullOrEmpty(_receivedPerson.Id))
            {
                throw new Exception("Id should not be null or empty.");
            }
        }
    }
}