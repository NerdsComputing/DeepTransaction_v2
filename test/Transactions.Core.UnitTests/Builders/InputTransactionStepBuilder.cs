using System;
using System.Linq.Expressions;
using Moq;
using Transactions.Core.TransactionSteps.Generic;

namespace Transactions.Core.UnitTests.Builders
{
    public class InputTransactionStepBuilder
    {
        public InputTransactionStep<TInput> Build<TInput>()
        {
            var mock = new Mock<InputTransactionStep<TInput>>();
            return mock.Object;
        }

        public Mock<InputTransactionStep<TInput>> BuildThrowableMock<TInput>
            (Expression<Action<InputTransactionStep<TInput>>> method)
        {
            var mock = new Mock<InputTransactionStep<TInput>>();
            mock.Setup(method).Throws<Exception>();
            return mock;
        }

        public InputTransactionStep<TInput> BuildThrowable<TInput>
            (Expression<Action<InputTransactionStep<TInput>>> method)
        {
            return BuildThrowableMock(method).Object;
        }
    }
}