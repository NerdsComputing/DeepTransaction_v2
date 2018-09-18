using Transactions.Console.Models;
using Transactions.Core.Generic;
using Transactions.Core.Generic.Steps;

namespace Transactions.Console.Transactions.Steps
{
    public class SetLastNameStep : TransactionStep<Person, Person>
    {
        private Person _person;

        public override Person Output => _person;

        public override void Initialize(Person input)
        {
            _person = input;
        }

        public override void Execute()
        {
            _person.LastName = "Random last name here.";
        }

        public override void Rollback()
        {
            _person.LastName = null;
        }
    }
}