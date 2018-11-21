using System;
using Moq;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions.Generic;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Generic
{
    public class InputTransactionTests : TransactionBaseTests
    {
        private readonly Mock<InputTransaction<int>> _instance;
        private readonly int _input;

        public InputTransactionTests()
        {
            _instance = new Mock<InputTransaction<int>>();
            _input = 1;
        }

        [Fact]
        public void Execute_Throws_WhenTransactionIsEmpty()
        {
            Action act = () => _instance.Object.Execute(_input);

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepReturns()
        {
            _instance.Object.ContinueWith(new MockTransactionStep<int, int>());

            Action act = () => _instance.Object.Execute(_input);

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_Calls_BeforeMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int>());

            _instance.Object.Execute(_input);

            _instance.Verify(instance => instance.BeforeExecution());
        }

        [Fact]
        public void Execute_Calls_AfterMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int>());

            _instance.Object.Execute(_input);

            _instance.Verify(instance => instance.AfterExecution());
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {BeforeExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute(_input));

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {ExecuteThrows = true});

            Act.Safe(() => _instance.Object.Execute(_input));

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {AfterExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute(_input));

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallRollback_WhenThereAreNoErrors()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int>());

            _instance.Object.Execute(_input);

            _instance.Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {BeforeExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute(_input));

            _instance.Verify(instance => instance.Rollback());
        }


        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {ExecuteThrows = true});

            Act.Safe(() => _instance.Object.Execute(_input));

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {AfterExecutionThrows = true});

            Act.Safe(() => _instance.Object.Execute(_input));

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInBeforeMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {BeforeExecutionThrows = true});

            Action act = () => _instance.Object.Execute(_input);

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInExecuteMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {ExecuteThrows = true});

            Action act = () => _instance.Object.Execute(_input);

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAfterMethod()
        {
            _instance.Object.ContinueWith(new MockInputTransactionStep<int> {AfterExecutionThrows = true});

            Action act = () => _instance.Object.Execute(_input);

            act.ShouldThrow<Exception>();
        }
    }
}