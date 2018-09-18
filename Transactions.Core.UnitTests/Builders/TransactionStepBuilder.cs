using System;
using System.Linq.Expressions;
using Moq;
using Transactions.Core.Generic.Steps;

namespace Transactions.Core.UnitTests.Builders
{
    public class TransactionStepBuilder
    {
        public TransactionStep<TInput, TOutput> Build<TInput, TOutput>()
        {
            var mock = new Mock<TransactionStep<TInput, TOutput>>();
            return mock.Object;
        }

        public TransactionStep Build()
        {
            var mock = new Mock<TransactionStep>();
            return mock.Object;
        }

        public ITransactionStep BuildThrowable(Expression<Action<TransactionStep>> expression)
        {
            var mock = new Mock<TransactionStep>();

            mock
                .Setup(expression)
                .Throws<Exception>();

            return mock.Object;
        }

        public Mock<ITransactionStep> BuildThrowableMock(Expression<Action<ITransactionStep>> expression)
        {
            var mock = new Mock<ITransactionStep>();

            mock
                .Setup(expression)
                .Throws<Exception>();

            return mock;
        }

        public TransactionStep<TInput, TOutput>
            BuildThrowable<TInput, TOutput>(Expression<Action<TransactionStep<TInput, TOutput>>> expression)
        {
            var mock = new Mock<TransactionStep<TInput, TOutput>>();

            mock
                .Setup(expression)
                .Throws<Exception>();

            return mock.Object;
        }

        public Mock<TransactionStep<TInput, TOutput>>
            BuildThrowableMock<TInput, TOutput>(Expression<Action<TransactionStep<TInput, TOutput>>> expression)
        {
            var mock = new Mock<TransactionStep<TInput, TOutput>>();

            mock
                .Setup(expression)
                .Throws<Exception>();

            return mock;
        }
    }
}