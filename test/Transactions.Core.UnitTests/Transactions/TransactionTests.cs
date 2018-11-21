using System;
using Moq;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions
{
    public class TransactionTests : TransactionBaseTests
    {
        private readonly Mock<Transaction> _instance;

        public TransactionTests()
        {
            _instance = new Mock<Transaction>();
        }

        [Fact]
        public void Execute_Throws_WhenTransactionIsEmpty()
        {
            Action act = _instance.Object.Execute;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsOutput()
        {
            _instance.Object.ContinueWith(new MockOutputTransactionStep<object>());

            Action act = _instance.Object.Execute;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsIO()
        {
            _instance
                .Object
                .ContinueWith(new MockOutputTransactionStep<object>())
                .ContinueWith(new MockTransactionStep<object, object>());

            Action act = _instance.Object.Execute;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Calls_BeforeMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.BeforeExecution());
        }

        [Fact]
        public void Execute_Calls_AfterMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.AfterExecution());
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {BeforeExecutionThrows = true});

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {ExecuteThrows = true});

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {AfterExecutionThrows = true});

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallRollback_WhenThereAreNoErrors()
        {
            _instance.Object.ContinueWith(new MockTransactionStep());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {BeforeExecutionThrows = true});

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {ExecuteThrows = true});

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {AfterExecutionThrows = true});

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {BeforeExecutionThrows = true});

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {ExecuteThrows = true});

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockTransactionStep {AfterExecutionThrows = true});

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }
    }
}