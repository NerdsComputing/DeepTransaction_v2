using Transactions.Console.Models;
using Transactions.Core;
using Transactions.Core.Generic;
using Transactions.Core.Generic.Steps;

namespace Transactions.Console.Transactions.Steps
{
    public class SetFirstNameStep : TransactionStep<Person, Person>
    {
        private Person _person;

        public override Person Output => _person;

        public override void Initialize(Person input)
        {
            _person = input;
        }

        public override void Execute()
        {
            _person.FirstName = "Random first name here.";
        }

        public override void Rollback()
        {
            _person.FirstName = null;
        }
    }
}