using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.OutputTransaction.ContinueWith
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
        public void Validate_Throws_WhenStepHasNoInputAndLastStepHasOutput()
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

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}