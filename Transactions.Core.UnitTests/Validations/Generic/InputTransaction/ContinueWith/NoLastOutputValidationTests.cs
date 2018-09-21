using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.InputTransaction.ContinueWith
{
    public class NoLastOutputValidationTests : BaseTransactionStepTests
    {
        private readonly NoLastOutputValidation _instance;
        private readonly ICollection<ITransactionStep> _steps;

        public NoLastOutputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new NoLastOutputValidation(_steps);
        }

        [Fact]
        public void Validate_Throws_WhenStepHasInputAndLastStepHasNoOutput()
        {
            _steps.Add(StepBuilder.Build());

            Action act = () => _instance.Validate(InputStepBuilder.Build<int>());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepHasInputAndLastStepHasNoOutput()
        {
            _steps.Add(StepBuilder.Build());

            Action act = () => _instance.Validate(InputStepBuilder.Build<int>());

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}