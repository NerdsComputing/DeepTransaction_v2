using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.OutputTransaction.ContinueWith
{
    public class FirstInputValidationTests : BaseTransactionStepTests
    {
        private readonly FirstInputValidation _instance;

        public FirstInputValidationTests()
        {
            _instance = new FirstInputValidation(new List<ITransactionStep>());
        }

        [Fact]
        public void Validate_Throws_WhenStepHasInputAndListIsEmpty()
        {
            Action act = () => _instance.Validate(InputStepBuilder.Build<int>());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepHasInputAndListIsEmpty()
        {
            Action act = () => _instance.Validate(InputStepBuilder.Build<int>());

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}