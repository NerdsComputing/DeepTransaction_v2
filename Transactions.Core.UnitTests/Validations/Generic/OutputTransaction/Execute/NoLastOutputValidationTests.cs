using System;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.OutputTransaction.Execute;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.OutputTransaction.Execute
{
    public class NoLastOutputValidationTests : BaseTransactionStepTests
    {
        private readonly NoLastOutputValidation _instance;
        private readonly IList<ITransactionStep> _steps;

        public NoLastOutputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new NoLastOutputValidation(_steps, BuildHasOutput());
        }

        [Fact]
        public void Validate_Throws_WhenLastStepHasNoOutput()
        {
            _steps.Add(StepBuilder.Build());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepHasNoOutput()
        {
            _steps.Add(StepBuilder.Build());

            Action act = _instance.Validate;

            var exception = act.ShouldThrow<WrongSuccessionException>();
            exception.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepHasOutput()
        {
            _steps.Add(OutputStepBuilder.Build<int>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }

        private static IHasOutput BuildHasOutput()
        {
            var hasInput = new Mock<IHasOutput>();

            hasInput
                .Setup(instance => instance.OutputType)
                .Returns(typeof(int));

            return hasInput.Object;
        }
    }
}