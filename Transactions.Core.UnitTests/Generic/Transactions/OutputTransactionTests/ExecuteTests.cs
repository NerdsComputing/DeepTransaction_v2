using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Shouldly;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.OutputTransactionTests
{
    public class ExecuteTests : BaseTransactionStepTests
    {
        private readonly Mock<OutputTransaction<int>> _instance;

        public ExecuteTests()
        {
            _instance = new Mock<OutputTransaction<int>>();
        }

        [Fact]
        public void Execute_Calls_ExecuteValidations()
        {
            MockOutputTransaction mockInstance = BuildMockInstance();

            mockInstance.Execute();

            mockInstance.GetMockExecuteValidations().Last().Verify(instance => instance.Validate());
        }

        [Fact]
        public void Execute_Calls_BeforeMethod()
        {
            _instance.Object.ContinueWith(OutputStepBuilder.Build<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.BeforeExecution());
        }

        [Fact]
        public void Execute_Calls_AfterMethod()
        {
            _instance.Object.ContinueWith(OutputStepBuilder.Build<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.AfterExecution());
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyBeforeMethod()
        {
            _instance.Object.ContinueWith(StepBuilder.BuildThrowable<int, int>(instance => instance.BeforeExecution()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyExecuteMethod()
        {
            _instance.Object.ContinueWith(StepBuilder.BuildThrowable<int, int>(instance => instance.Execute()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallAfterMethod_WhenAnErrorOccursInAnyAfterMethod()
        {
            _instance.Object.ContinueWith(StepBuilder.BuildThrowable<int, int>(instance => instance.AfterExecution()));

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.AfterExecution(), Times.Never);
        }

        [Fact]
        public void Execute_DoesNotCallRollback_WhenThereAreNoErrors()
        {
            _instance.Object.ContinueWith(OutputStepBuilder.Build<int>());

            _instance.Object.Execute();

            _instance.Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyBeforeMethod()
        {
            var wrongStep = OutputStepBuilder.BuildThrowable<int>(instance => instance.BeforeExecution());
            _instance.Object.ContinueWith(wrongStep);

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_CallsRollback_WhenAnErrorOccursInAnyAfterMethod()
        {
            var wrongStep = OutputStepBuilder.BuildThrowable<int>(instance => instance.AfterExecution());
            _instance.Object.ContinueWith(wrongStep);

            Act.Safe(_instance.Object.Execute);

            _instance.Verify(instance => instance.Rollback());
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInBeforeMethod()
        {
            var wrongStep = OutputStepBuilder.BuildThrowable<int>(instance => instance.BeforeExecution());
            _instance.Object.ContinueWith(wrongStep);

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAnyExecuteMethod()
        {
            var wrongStep = OutputStepBuilder.BuildThrowable<int>(instance => instance.Execute());
            _instance.Object.ContinueWith(wrongStep);

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenAnErrorOccursInAnyAfterMethod()
        {
            var wrongStep = OutputStepBuilder.BuildThrowable<int>(instance => instance.AfterExecution());
            _instance.Object.ContinueWith(wrongStep);

            Action act = _instance.Object.Execute;

            act.ShouldThrow<Exception>();
        }

        private MockOutputTransaction BuildMockInstance()
        {
            var mockInstance = new MockOutputTransaction();
            mockInstance.ContinueWith(OutputStepBuilder.Build<int>());
            mockInstance.AddExecuteValidation(new Mock<IValidation>());
            return mockInstance;
        }

        private class MockOutputTransaction : OutputTransaction<int>
        {
            private readonly ICollection<IValidation> _executeValidations;
            private readonly ICollection<Mock<IValidation>> _mockExecuteValidations;

            protected override IEnumerable<IValidation> ExecuteValidations => _executeValidations;

            public MockOutputTransaction()
            {
                _executeValidations = new List<IValidation>();
                _mockExecuteValidations = new List<Mock<IValidation>>();
            }

            public void AddExecuteValidation(Mock<IValidation> validation)
            {
                _executeValidations.Add(validation.Object);
                _mockExecuteValidations.Add(validation);
            }

            public IEnumerable<Mock<IValidation>> GetMockExecuteValidations()
            {
                return _mockExecuteValidations;
            }
        }
    }
}