using System;
using Transactions.Core.TransactionSteps;

namespace Transactions.Core.UnitTests.Mocks.TransactionSteps
{
    public class MockTransactionStep : TransactionStep
    {
        public bool BeforeExecutionThrows { private get; set; }
        public bool ExecuteThrows { private get; set; }
        public bool AfterExecutionThrows { private get; set; }

        public override void BeforeExecution()
        {
            if (BeforeExecutionThrows)
            {
                throw new Exception();
            }
        }

        public override void Execute()
        {
            if (ExecuteThrows)
            {
                throw new Exception();
            }
        }

        public override void AfterExecution()
        {
            if (AfterExecutionThrows)
            {
                throw new Exception();
            }
        }
    }
}