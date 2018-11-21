using System;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions;
using Transactions.Core.Transactions.Generic.Validations.OutputTransaction.Execute;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Generic.Validations.OutputTransaction.Execute
{
    public class WrongLastOutputTests : ValidationBaseTests
    {
        private readonly IValidation _instance;

        public WrongLastOutputTests()
        {
            _instance = new WrongLastOutput<int>(Entities);
        }

        [Fact]
        public void Validate_Throws_WhenLastStepIsVoid()
        {
            Entities.Add(new MockTransactionStep());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepIsVoid()
        {
            Entities.Add(new MockTransactionStep());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsInput()
        {
            Entities.Add(new MockInputTransactionStep<int>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_ThrownExceptionHasMessage_WhenLastStepIsInput()
        {
            Entities.Add(new MockInputTransactionStep<int>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsOutputAndHasWrongOutput()
        {
            Entities.Add(new MockOutputTransactionStep<string>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_ThrownExceptionHasMessage_WhenLastStepIsOutputAndHasWrongOutput()
        {
            Entities.Add(new MockOutputTransactionStep<string>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Execute_DoesNotThrow_WhenLastStepIsOutputAndHasRightOutput()
        {
            Entities.Add(new MockOutputTransactionStep<int>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }

        [Fact]
        public void Execute_Throws_WhenLastStepIsIOAndHasWrongOutput()
        {
            Entities.Add(new MockTransactionStep<int, string>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Execute_ThrownExceptionHasMessage_WhenLastStepIsIOAndHasWrongOutput()
        {
            Entities.Add(new MockTransactionStep<int, string>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Execute_DoesNotThrow_WhenLastStepIsIOAndHasRightOutput()
        {
            Entities.Add(new MockTransactionStep<object, int>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }
    }
}