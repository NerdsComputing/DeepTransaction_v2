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
    public class WrongLastOutputTests : ValidationBaseTests
    {
        private readonly IValidation _instance;

        public WrongLastOutputTests()
        {
            _instance = new WrongLastOutput<int>(Entities);
        }

        [Fact]
        public void Validate_Throws_WhenLastStepIsOutputAndHasWrongOutput()
        {
            Entities.Add(new MockOutputTransactionStep<string>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepIsOutputAndHasWrongOutput()
        {
            Entities.Add(new MockOutputTransactionStep<string>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_Throws_WhenLastStepIsIOAndHasWrongOutput()
        {
            Entities.Add(new MockTransactionStep<string, string>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepIsIOAndHasWrongOutput()
        {
            Entities.Add(new MockTransactionStep<string, string>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsInput()
        {
            Entities.Add(new MockInputTransactionStep<int>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsVoid()
        {
            Entities.Add(new MockTransactionStep());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }
    }
}