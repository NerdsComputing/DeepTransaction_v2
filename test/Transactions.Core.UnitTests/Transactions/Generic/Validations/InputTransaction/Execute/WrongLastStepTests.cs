using System;
using Moq;
using Shouldly;
using Transactions.Core.Contract.TransactionSteps;
using Transactions.Core.Contract.TransactionSteps.Generic;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions;
using Transactions.Core.Transactions.Generic.Validations.InputTransaction.Execute;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Generic.Validations.InputTransaction.Execute
{
    public class WrongLastStepValidationTests : ValidationBaseTests
    {
        private readonly IValidation _instance;

        public WrongLastStepValidationTests()
        {
            _instance = new WrongLastStep(Entities);
        }

        [Fact]
        public void Validate_Throws_WhenLastStepHasOutput()
        {
            Entities.Add(new MockTransactionStep<int, int>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownException_HasMessage()
        {
            Entities.Add(new MockTransactionStep<int, int>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepHasNoOutput()
        {
            Entities.Add(new MockTransactionStep());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }
    }
}