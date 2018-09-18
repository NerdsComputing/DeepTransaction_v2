using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.InputTransaction.ContinueWith
{
    public class NoInputValidationTests : BaseTransactionStepTests
    {
        private readonly NoInputValidation _instance;
        private readonly ICollection<ITransactionStep> _steps;

        public NoInputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new NoInputValidation(_steps);
        }

        [Fact]
        public void Validate_Throws_WhenSepHasNoInputAndLastStepHasOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate(StepBuilder.Build());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepHasNoInputAndLastStepHasOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate(StepBuilder.Build());

            var exception = act.ShouldThrow<WrongSuccessionException>();
            exception.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenStepHasInputAndLastStepHasOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate(StepBuilder.Build<int, int>());

            act.ShouldNotThrow();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenStepHasNoInputAndListIsEmpty()
        {
            Action act = () => _instance.Validate(OutputStepBuilder.Build<int>());

            act.ShouldNotThrow();
        }
    }
}