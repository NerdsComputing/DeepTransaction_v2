using System;
using Moq;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions.Generic;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Generic
{
    public class OutputTransactionTests : TransactionBaseTests
    {
        private readonly Mock<OutputTransaction<int>> _instance;

        public OutputTransactionTests()
        {
            _instance = new Mock<OutputTransaction<int>>();
        }

        [Fact]
        public void Execute_Throws_WhenTransactionIsEmpty()
        {
            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsVoid()
        {
            _instance.Object.ContinueWith(new MockTransactionStep());

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsInput()
        {
            _instance
                .Object
                .ContinueWith(new MockOutputTransactionStep<int>())
                .ContinueWith(new MockInputTransactionStep<int>());

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsOutputAndHasWrongOutput()
        {
            _instance
                .Object
                .ContinueWith(new MockTransactionStep())
                .ContinueWith(new MockOutputTransactionStep<string>());

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsIOAndHasWrongOutput()
        {
            _instance
                .Object
                .ContinueWith(new MockOutputTransactionStep<int>())
                .ContinueWith(new MockTransactionStep<int, string>());

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Calls_BeforeMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.BeforeExecution());
        }

        [Fact]
        public void Execute_Calls_AfterMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.AfterExecution());
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {BeforeExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute());

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {ExecuteThrows = true});

            Act.Safe(() => _instance.Object.Execute());

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {AfterExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute());

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallRollback_WhenThereAreNoErrors()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {BeforeExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute());

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {ExecuteThrows = true});

            Act.Safe(() => _instance.Object.Execute());

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {AfterExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute());

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {BeforeExecutionThrows = true});

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {ExecuteThrows = true});

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<int> {AfterExecutionThrows = true});

            Action act = () => _instance.Object.Execute();

            act.ShouldThrow<Exception>();
        }
    }
}