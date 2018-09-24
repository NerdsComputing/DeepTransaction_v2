using System;
using Transactions.Core.TransactionSteps.Generic;

namespace Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic
{
    public class MockInputTransactionStep<TInput> : InputTransactionStep<TInput>
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

        public override void Execute(TInput input)
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