using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.InputTransaction.ContinueWith
{
    public class WrongFirstInputValidationTests : BaseTransactionStepTests
    {
        private readonly NoFirstInputValidation _instance;

        public WrongFirstInputValidationTests()
        {
            _instance = new NoFirstInputValidation(new List<ITransactionStep>(), new InputTransaction<int>());
        }

        [Fact]
        public void Validate_Throws_WhenStepHasNoInputAndListIsEmpty()
        {
            Action act = () => _instance.Validate(StepBuilder.Build());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepHasNoInputAndListIsEmpty()
        {
            Action act = () => _instance.Validate(StepBuilder.Build());

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}