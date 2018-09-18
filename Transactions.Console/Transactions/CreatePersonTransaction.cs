using Transactions.Console.Models;
using Transactions.Console.Transactions.Steps;
using Transactions.Core.Generic.Transactions;

namespace Transactions.Console.Transactions
{
    public class CreatePersonTransaction : Transaction<string, Person>
    {
        public CreatePersonTransaction(string personId) : base(personId)
        {
            ContinueWith(new CreatePersonStep(null))
                .ContinueWith(new SetFirstNameStep())
                .ContinueWith(new SetLastNameStep())
                .ContinueWith(new ValidatePersonStep())
                .ContinueWith(new PrintIdStep())
                .ContinueWith(new PrintFirstNameStep())
                .ContinueWith(new PrintLastNameStep());
        }
    }
}