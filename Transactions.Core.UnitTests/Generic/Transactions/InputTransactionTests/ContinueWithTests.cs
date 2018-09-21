using System.Collections.Generic;
using Moq;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.InputTransactionTests
{
    public class ContinueWithTests : BaseTransactionStepTests
    {
        private readonly MockInputTransaction _instance;

        public ContinueWithTests()
        {
            _instance = new MockInputTransaction();
        }

        [Fact]
        public void ContinueWith_Calls_ContinueValidations()
        {
            var mockValidation = new Mock<IValidation<ITransactionStep>>();
            _instance.AddContinueValidation(mockValidation.Object);

            _instance.ContinueWith(InputStepBuilder.Build<int>());

            mockValidation.Verify(instance => instance.Validate(It.IsAny<ITransactionStep>()));
        }

        private class MockInputTransaction : InputTransaction<int>
        {
            private readonly ICollection<IValidation<ITransactionStep>> _continueValidations;

            protected override IEnumerable<IValidation<ITransactionStep>> ContinueValidations => _continueValidations;

            public MockInputTransaction()
            {
                _continueValidations = new List<IValidation<ITransactionStep>>();
            }

            public void AddContinueValidation(IValidation<ITransactionStep> validation)
            {
                _continueValidations.Add(validation);
            }
        }
    }
}