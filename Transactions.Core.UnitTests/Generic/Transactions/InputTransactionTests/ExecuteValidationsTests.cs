using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations;
using Transactions.Core.Validations.Generic.InputTransaction.Execute;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.InputTransactionTests
{
    public class ExecuteValidationsTests
    {
        private readonly MockInputTransaction _instance;

        public ExecuteValidationsTests()
        {
            _instance = new MockInputTransaction();
        }
        
        [Fact]
        public void ExecuteValidations_Contains_LastOutputValidation()
        {
            IEnumerable<IValidation> validations = _instance.GetExecuteValidations();
            
            validations.ShouldContain(instance => instance is LastOutputValidation);
        }
        
        private class MockInputTransaction : InputTransaction<int>
        {
            public IEnumerable<IValidation> GetExecuteValidations()
            {
                return ExecuteValidations;
            }
        }
    }
}