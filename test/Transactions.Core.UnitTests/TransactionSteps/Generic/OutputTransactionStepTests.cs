using Moq;
using Transactions.Core.Contract;
using Transactions.Core.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.TransactionSteps.Generic
{
    public class OutputTransactionStepTests
    {
        private readonly Mock<OutputTransactionStep<int>> _instance;

        public OutputTransactionStepTests()
        {
            _instance = new Mock<OutputTransactionStep<int>>();
        }

        [Fact]
        public void Execute_Calls_GenericExecute()
        {
            ((IOutputExecute) _instance.Object).Execute();

            _instance.Verify(instance => instance.Execute());
        }
    }
}