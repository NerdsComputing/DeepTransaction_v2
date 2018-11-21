using Moq;
using Transactions.Core.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.TransactionSteps.Generic
{
    public class TransactionStepTests
    {
        private readonly Mock<TransactionStep<int, int>> _instance;

        public TransactionStepTests()
        {
            _instance = new Mock<TransactionStep<int, int>>();
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