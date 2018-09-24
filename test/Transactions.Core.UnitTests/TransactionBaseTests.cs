using Transactions.Core.UnitTests.Builders;

namespace Transactions.Core.UnitTests
{
    public abstract class TransactionBaseTests
    {
        protected readonly TransactionStepBuilder StepBuilder;
        protected readonly InputTransactionStepBuilder InputStepBuilder;
        protected readonly OutputTransactionStepBuilder OutputStepBuilder;

        protected TransactionBaseTests()
        {
            StepBuilder = new TransactionStepBuilder();
            InputStepBuilder = new InputTransactionStepBuilder();
            OutputStepBuilder = new OutputTransactionStepBuilder();
        }
    }
}