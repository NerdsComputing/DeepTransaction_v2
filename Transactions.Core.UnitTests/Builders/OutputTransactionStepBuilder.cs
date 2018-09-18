using System;
using System.Linq.Expressions;
using Moq;
using Transactions.Core.Generic.Steps;

namespace Transactions.Core.UnitTests.Builders
{
    public class OutputTransactionStepBuilder
    {
        public OutputTransactionStep<TOutput> Build<TOutput>()
        {
            var mock = new Mock<OutputTransactionStep<TOutput>>();
            return mock.Object;
        }

        public OutputTransactionStep<TOutput> BuildThrowable<TOutput>
            (Expression<Action<OutputTransactionStep<TOutput>>> method)
        {
            var mock = new Mock<OutputTransactionStep<TOutput>>();
            mock.Setup(method).Throws<Exception>();
            return mock.Object;
        }

        public Mock<OutputTransactionStep<TOutput>> BuildThrowableMock<TOutput>
            (Expression<Action<OutputTransactionStep<TOutput>>> method)
        {
            var mock = new Mock<OutputTransactionStep<TOutput>>();
            mock.Setup(method).Throws<Exception>();
            return mock;
        }
    }
}