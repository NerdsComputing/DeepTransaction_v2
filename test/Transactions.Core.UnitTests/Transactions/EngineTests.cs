using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Shouldly;
using Transactions.Core.Contract;
using Transactions.Core.Contract.TransactionSteps;
using Transactions.Core.Transactions;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions
{
    public class EngineTests : TransactionBaseTests
    {
        private readonly IEngine _instance;
        private readonly IList<ITransactionEntity> _steps;
        private readonly IList<Mock<ITransactionStep>> _mockSteps;

        public EngineTests()
        {
            _steps = new List<ITransactionEntity>();
            _mockSteps = new List<Mock<ITransactionStep>>();
            _instance = new Engine(_steps);
            PopulateTransactionSteps();
        }

        [Fact]
        public void Execute_CallsBeforeExecution_FromEachStep()
        {
            _instance.Execute();

            VerifyInvocations(instance => instance.BeforeExecution(), Times.Once());
        }

        [Fact]
        public void Execute_CallsExecute_FromEachStep()
        {
            _instance.Execute();

            VerifyInvocations(instance => instance.Execute(), Times.Once());
        }

        [Fact]
        public void Execute_CallsAfterExecution_FromEachStep()
        {
            _instance.Execute();

            VerifyInvocations(instance => instance.AfterExecution(), Times.Once());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherBeforeMethods_WhenAnErrorOccursInBeforeMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.BeforeExecution()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.BeforeExecution(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherExecuteMethods_WhenAnErrorOccursInBeforeMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.BeforeExecution()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.Execute(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherAfterMethods_WhenAnErrorOccursInBeforeMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.BeforeExecution()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.AfterExecution(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherBeforeMethods_WhenAnErrorOccursInExecuteMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.Execute()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.BeforeExecution(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherExecuteMethods_WhenAnErrorOccursInExecuteMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.Execute()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.Execute(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherAfterMethods_WhenAnErrorOccursInExecuteMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.Execute()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.AfterExecution(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherBeforeMethods_WhenAnErrorOccursInAfterMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.AfterExecution()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.BeforeExecution(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherExecuteMethods_WhenAnErrorOccursInAfterMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.AfterExecution()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.Execute(), Times.Never());
        }

        [Fact]
        public void Execute_DoesNotCallFurtherAfterMethods_WhenAnErrorOccursInAfterMethod()
        {
            InsertTransactionStep(StepBuilder.BuildThrowableMock(instance => instance.AfterExecution()), 0);

            Act.Safe(_instance.Execute);

            VerifyInvocations(_mockSteps.Skip(1), instance => instance.AfterExecution(), Times.Never());
        }

        [Fact]
        public void Execute_RollbackAllExecutedSteps_WhenAnErrorOccursInAnyBeforeMethod()
        {
            AddTransactionSteps(BuildThrowableStep(instance => instance.BeforeExecution()));

            Act.Safe(_instance.Execute);

            VerifyInvocations(instance => instance.Rollback(), Times.Once());
        }

        [Fact]
        public void Execute_RollbackAllExecutedSteps_WhenAnErrorOccursInAnyExecuteMethod()
        {
            AddTransactionSteps(BuildThrowableStep(instance => instance.Execute()));

            Act.Safe(_instance.Execute);

            VerifyInvocations(instance => instance.Rollback(), Times.Once());
        }

        [Fact]
        public void Execute_RollbackAllExecutedSteps_WhenAnErrorOccursInAnyAfterMethod()
        {
            AddTransactionSteps(BuildThrowableStep(instance => instance.AfterExecution()));

            Act.Safe(_instance.Execute);

            VerifyInvocations(instance => instance.Rollback(), Times.Once());
        }

        [Fact]
        public void Execute_RollbackIsNotCalledForNotExecutedStep_WhenAnErrorOccursInAnyBeforeMethod()
        {
            var throwableStep = BuildThrowableStep(instance => instance.BeforeExecution());
            AddTransactionSteps(throwableStep, new Mock<ITransactionStep>());

            Act.Safe(_instance.Execute);

            _mockSteps.Last().Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_RollbackIsNotCalledForNotExecutedStep_WhenAnErrorOccursInAnyExecuteMethod()
        {
            var throwableStep = BuildThrowableStep(instance => instance.Execute());
            AddTransactionSteps(throwableStep, new Mock<ITransactionStep>());

            Act.Safe(_instance.Execute);

            _mockSteps.Last().Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_RollbackIsNotCalledForNotExecutedStep_WhenErrorOccursInAnyAfterMethod()
        {
            var throwableStep = BuildThrowableStep(instance => instance.AfterExecution());
            AddTransactionSteps(throwableStep, new Mock<ITransactionStep>());

            Act.Safe(_instance.Execute);

            _mockSteps.Last().Verify(instance => instance.Rollback(), Times.Never);
        }

        [Fact]
        public void Execute_Throws_WhenErrorOccursInAnyBeforeMethod()
        {
            AddTransactionSteps(BuildThrowableStep(instance => instance.BeforeExecution()));

            Action act = _instance.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenErrorOccursInAnyExecuteMethod()
        {
            AddTransactionSteps(BuildThrowableStep(instance => instance.Execute()));

            Action act = _instance.Execute;

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_Throws_WhenErrorOccursInAnyAfterMethod()
        {
            AddTransactionSteps(BuildThrowableStep(instance => instance.AfterExecution()));

            Action act = _instance.Execute;

            act.ShouldThrow<Exception>();
        }

        private void VerifyInvocations(Expression<Action<ITransactionStep>> condition, Times times)
        {
            VerifyInvocations(_mockSteps, condition, times);
        }

        private void VerifyInvocations(
            IEnumerable<Mock<ITransactionStep>> steps,
            Expression<Action<ITransactionStep>> condition,
            Times times)
        {
            foreach (Mock<ITransactionStep> step in steps)
            {
                step.Verify(condition, times);
            }
        }

        private void AddTransactionSteps(params Mock<ITransactionStep>[] steps)
        {
            foreach (var mock in steps)
            {
                _mockSteps.Add(mock);
                _steps.Add(mock.Object);
            }
        }

        private void InsertTransactionStep(Mock<ITransactionStep> step, int index)
        {
            _mockSteps.Insert(index, step);
            _steps.Insert(index, step.Object);
        }

        private void PopulateTransactionSteps()
        {
            AddTransactionSteps(new Mock<ITransactionStep>());
            AddTransactionSteps(new Mock<ITransactionStep>());
        }

        private static Mock<ITransactionStep> BuildThrowableStep(Expression<Action<ITransactionStep>> predicate)
        {
            var mock = new Mock<ITransactionStep>();
            mock.Setup(predicate).Throws<Exception>();
            return mock;
        }
    }
}