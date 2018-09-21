using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.InputTransaction.ContinueWith
{
    public class WrongInputValidationTests : BaseTransactionStepTests
    {
        private readonly WrongInputValidation _instance;
        private readonly ICollection<ITransactionStep> _steps;

        public WrongInputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new WrongInputValidation(_steps);
        }

        [Fact]
        public void Validate_Throws_WhenStepInputDiffersFromLastStepOutput()
        {
            _steps.Add(StepBuilder.Build<int, int>());

            Action act = () => _instance.Validate(StepBuilder.Build<float, int>());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepInputDiffersFromLastStepOutput()
        {
            _steps.Add(StepBuilder.Build<int, int>());

            Action act = () => _instance.Validate(StepBuilder.Build<float, int>());

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}