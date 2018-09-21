using System.Collections.Generic;
using Moq;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.OutputTransactionTests
{
    public class ContinueWithTests : BaseTransactionStepTests
    {
        private readonly MockOutputTransaction _instance;

        public ContinueWithTests()
        {
            _instance = new MockOutputTransaction();
        }

        [Fact]
        public void ContinueWith_Calls_ContinueValidations()
        {
            var validation = new Mock<IValidation<ITransactionStep>>();
            _instance.AddContinueValidation(validation.Object);

            _instance.ContinueWith(OutputStepBuilder.Build<int>());

            validation.Verify(instance => instance.Validate(It.IsAny<ITransactionStep>()));
        }

        private class MockOutputTransaction : OutputTransaction<int>
        {
            private readonly ICollection<IValidation<ITransactionStep>> _continueValidations;

            protected override IEnumerable<IValidation<ITransactionStep>> ContinueValidations => _continueValidations;

            public MockOutputTransaction()
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