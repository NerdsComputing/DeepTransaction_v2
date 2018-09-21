using System.Collections.Generic;
using System.Linq;
using Transactions.Console.Models;
using Transactions.Console.Transactions.Steps;
using Transactions.Core;
using Transactions.Core.Generic.Steps;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations;
using Transactions.Core.Validations.Generic;
using Transactions.Linq.Generic.InputTransactionExtensions;
using Transactions.Linq.TransactionExtensions;

namespace Transactions.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputTransaction = new OutputTransaction<Person>();
            var transaction = new Transaction<Person, Person>();
            var inputTransaction = new InputTransaction<string>("My id here.");
            var voidTransaction = new Transaction();

            var otherTransaction = OutputTransaction
                .Create<Person>()
                .ContinueWith(Transaction.Create<Person, int>());

            otherTransaction.Execute();

            inputTransaction
                .ContinueWith(new SetFirstNameStep())
                .ContinueWith(new SetLastNameStep())
                .ContinueWith(new Transaction())
//                .SwitchContinueWith(new Transaction<Person, Person>())
                .Execute();

            inputTransaction
                .ContinueWith(null)
                .SwitchContinueWith(new Transaction())
                .ContinueWith(null)
                .ContinueWith(null)
                .SwitchContinueWith(null)
                .ContinueWith(null);
        }

        private static ITransactionStep BuildValidatePersonTransaction()
        {
            return Transaction
                .Create<Person, Person>()
                .ContinueWith(new ValidatePersonStep());
        }

        private static ITransactionStep BuildPrintPersonTransaction()
        {
            return InputTransaction
                .Create<Person>()
                .ContinueWith(new PrintIdStep())
                .ContinueWith(new PrintFirstNameStep())
                .ContinueWith(new PrintLastNameStep());
        }
    }

    public class CustomTransaction<TInput> : InputTransaction<TInput>
    {
        protected override IEnumerable<IValidation> ExecuteValidations { get; }
        protected override IEnumerable<IValidation<ITransactionStep>> ContinueValidations { get; }

        public CustomTransaction()
        {
            ExecuteValidations = new List<IValidation> {new CustomValidation()};
            ContinueValidations = new List<IValidation<ITransactionStep>>();
        }
    }

    public class CustomValidation : IValidation
    {
        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}