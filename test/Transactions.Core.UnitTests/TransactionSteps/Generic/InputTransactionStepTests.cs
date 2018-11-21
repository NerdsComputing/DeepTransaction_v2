using Moq;
using Transactions.Core.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.TransactionSteps.Generic
{
    public class InputTransactionStepTests
    {
        private readonly Mock<InputTransactionStep<int>> _instance;

        public InputTransactionStepTests()
        {
            _instance = new Mock<InputTransactionStep<int>>();
        }

        [Fact]
        public void Execute_Calls_GenericExecute()
        {
            var expected = 1;

            _instance.Object.Execute((object) expected);

            _instance.Verify(instance => instance.Execute(expected));
        }
    }
}