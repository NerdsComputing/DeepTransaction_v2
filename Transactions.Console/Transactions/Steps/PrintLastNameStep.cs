using Transactions.Console.Models;
using Transactions.Core.Generic.Steps;

namespace Transactions.Console.Transactions.Steps
{
    public class PrintLastNameStep : InputTransactionStep<Person>
    {
        private Person _receivedPerson;

        public override void Initialize(Person input)
        {
            _receivedPerson = input;
        }

        public override void Execute()
        {
            System.Console.WriteLine($"Last name: {_receivedPerson.LastName}");
        }
    }
}