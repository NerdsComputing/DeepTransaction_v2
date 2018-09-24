using System;
using Transactions.Core.TransactionSteps.Generic;

namespace Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic
{
    public class MockOutputTransactionStep<TOutput> : OutputTransactionStep<TOutput>
    {
        public bool BeforeExecutionThrows { private get; set; }
        public bool ExecuteThrows { private get; set; }
        public bool AfterExecutionThrows { private get; set; }

        public MockOutputTransactionStep()
        {
            BeforeExecutionThrows = false;
            ExecuteThrows = false;
            AfterExecutionThrows = false;
        }

        public override void BeforeExecution()
        {
            if (BeforeExecutionThrows)
            {
                throw new Exception();
            }
        }

        public override TOutput Execute()
        {
            if (ExecuteThrows)
            {
                throw new Exception();
            }

            return default(TOutput);
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