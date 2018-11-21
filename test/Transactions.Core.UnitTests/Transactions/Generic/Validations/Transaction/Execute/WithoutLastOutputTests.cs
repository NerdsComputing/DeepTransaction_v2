using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Contract;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions;
using Transactions.Core.Transactions.Generic.Validations.Transaction.Execute;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Generic.Validations.Transaction.Execute
{
    public class WithoutLastOutputTests : ValidationBaseTests
    {
        private readonly IValidation _instance;

        public WithoutLastOutputTests()
        {
            _instance = new WithoutLastOutput(Entities);
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
        public void Validate_Throws_WhenLastStepIsInput()
        {
            Entities.Add(new MockInputTransactionStep<object>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepIsInput()
        {
            Entities.Add(new MockInputTransactionStep<object>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsOutput()
        {
            Entities.Add(new MockOutputTransactionStep<object>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsIO()
        {
            Entities.Add(new MockTransactionStep<object, object>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }
    }
}