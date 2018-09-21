using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.OutputTransaction.ContinueWith
{
    public class InputValidationTests : BaseTransactionStepTests
    {
        private readonly InputValidation _instance;
        private readonly ICollection<ITransactionStep> _steps;

        public InputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new InputValidation(_steps);
        }

        [Fact]
        public void Validate_Throws_WhenStepInputDiffersFromLastStepOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate(InputStepBuilder.Build<float>());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepInputDiffersFromLastStepOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate(InputStepBuilder.Build<float>());

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}