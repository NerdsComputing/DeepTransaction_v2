using System;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.OutputTransaction.Execute;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.OutputTransaction.Execute
{
    public class WrongLastOutputValidationTests : BaseTransactionStepTests
    {
        private readonly WrongLastOutputValidation _instance;
        private readonly IList<ITransactionStep> _steps;

        public WrongLastOutputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new WrongLastOutputValidation(_steps, BuildHasOutput());
        }

        [Fact]
        public void Validate_Throws_WhenLastStepHasWrongOutput()
        {
            _steps.Add(OutputStepBuilder.Build<float>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenLastStepHasWrongOutput()
        {
            _steps.Add(OutputStepBuilder.Build<float>());

            Action act = _instance.Validate;

            var exception = act.ShouldThrow<WrongSuccessionException>();
            exception.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepHasRightOutput()
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