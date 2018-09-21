using System;
using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations;
using Transactions.Core.Validations.Generic.InputTransaction.Execute;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.InputTransaction.Execute
{
    public class LastOutputValidationTests : BaseTransactionStepTests
    {
        private readonly IValidation _instance;
        private readonly ICollection<ITransactionStep> _steps;

        public LastOutputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new LastOutputValidation(_steps);
        }

        [Fact]
        public void Validate_Throws_WhenLastStepHasOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate();

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepHasOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = () => _instance.Validate();

            var thrownException = act.ShouldThrow<WrongSuccessionException>();
            thrownException.Message.ShouldNotBeEmpty();
        }
    }
}