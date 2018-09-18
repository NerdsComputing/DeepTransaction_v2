using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations;
using Transactions.Core.Validations.Generic.OutputTransaction.Execute;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.OutputTransactionTests
{
    public class ExecuteValidationsTests
    {
        private readonly MockOutputTransaction _instance;

        public ExecuteValidationsTests()
        {
            _instance = new MockOutputTransaction();
        }

        [Fact]
        public void ExecuteValidations_Contains_NoLastOutputValidation()
        {
            IEnumerable<IValidation> validations = _instance.GetExecuteValidations();

            validations.ShouldContain(instance => instance is NoLastOutputValidation);
        }

        [Fact]
        public void ExecuteValidations_Contains_WrongLastOutputValidation()
        {
            IEnumerable<IValidation> validations = _instance.GetExecuteValidations();

            validations.ShouldContain(instance => instance is WrongLastOutputValidation);
        }

        private class MockOutputTransaction : OutputTransaction<int>
        {
            public IEnumerable<IValidation> GetExecuteValidations()
            {
                return ExecuteValidations;
            }
        }
    }
}