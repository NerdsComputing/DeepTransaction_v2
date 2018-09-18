using System;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.InputTransactionTests
{
    public class ExecuteTests : BaseTransactionStepTests
    {
        private readonly Mock<InputTransaction<int>> _instance;

        public ExecuteTests()
        {
            _instance = new Mock<InputTransaction<int>>();
        }

        [Fact]
        public void Execute_Calls_ExecuteValidations()
        {
            var mockInstance = new MockInputTransaction();
            var mockValidation = new Mock<IValidation>();
            mockInstance.AddExecuteValidation(mockValidation.Object);

            mockInstance.Execute();

            mockValidation.Verify(instance => instance.Validate());
        }

        [Fact]
        public void Execute_Calls_BeforeMethod()
        {
            var mockInstance = new Mock<InputTransaction<int>>();

            mockInstance.Object.Execute();

            mockInstance.Verify(instance => instance.BeforeExecution());
        }

        [Fact]
        public void Execute_Calls_AfterMethod()
        {
            var mockInstance = new Mock<InputTransaction<int>>();

            mockInstance.Object.Execute();

            mockInstance.Verify(instance => instance.AfterExecution());
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(InputStepBuilder.BuildThrowable<int>(instance => instance.BeforeExecution()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(InputStepBuilder.BuildThrowable<int>(instance => instance.Execute()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(InputStepBuilder.BuildThrowable<int>(instance => instance.Execute()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallRollback_WhenThereAreNoErrors()
        {
            _instance.Object.ContinueWith(InputStepBuilder.Build<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(StepBuilder.BuildThrowable(instance => instance.BeforeExecution()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }


        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(StepBuilder.BuildThrowable(instance => instance.Execute()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(StepBuilder.BuildThrowable(instance => instance.AfterExecution()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInBeforeMethod()
        {
            _instance.Object.ContinueWith(InputStepBuilder.BuildThrowable<int>(instance => instance.BeforeExecution()));

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOcccursInExecuteMethod()
        {
            _instance.Object.ContinueWith(InputStepBuilder.BuildThrowable<int>(instance => instance.Execute()));

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOcccursInAfterMethod()
        {
            _instance.Object.ContinueWith(InputStepBuilder.BuildThrowable<int>(instance => instance.AfterExecution()));

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        private class MockInputTransaction : InputTransaction<int>
        {
            private readonly ICollection<IValidation> _executeValidations;

            protected override IEnumerable<IValidation> ExecuteValidations => _executeValidations;

            public MockInputTransaction()
            {
                _executeValidations = new List<IValidation>();
            }

            public void AddExecuteValidation(IValidation validation)
            {
                _executeValidations.Add(validation);
            }
        }
    }
}