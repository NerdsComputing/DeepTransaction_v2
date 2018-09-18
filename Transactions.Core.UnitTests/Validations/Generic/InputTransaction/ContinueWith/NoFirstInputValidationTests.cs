using System;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Validations.Generic.InputTransaction.ContinueWith
{
    public class NoFirstInputValidationTests : BaseTransactionStepTests
    {
        private readonly NoFirstInputValidation _instance;
        private readonly ICollection<ITransactionStep> _steps;

        public NoFirstInputValidationTests()
        {
            _steps = new List<ITransactionStep>();
            _instance = new NoFirstInputValidation(_steps, BuildHasInput());
        }

        [Fact]
        public void Validate_Throws_WhenListIsEmptyAndStepHasNoInput()
        {
            TransactionStep wrongStep = StepBuilder.Build();

            Action act = () => _instance.Validate(wrongStep);

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenListIsEmptyAndStepHasNoInput()
        {
            TransactionStep wrongStep = StepBuilder.Build();

            Action act = () => _instance.Validate(wrongStep);

            var exception = act.ShouldThrow<WrongSuccessionException>();
            exception.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenListIsNotEmptyAndStepHasNoInput()
        {
            _steps.Add(InputStepBuilder.Build<int>());
            var otherStep = StepBuilder.Build();

            Action act = () => _instance.Validate(otherStep);

            act.ShouldNotThrow();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenListIsNotEmptyAndStepHasInput()
        {
            _steps.Add(InputStepBuilder.Build<int>());

            Action act = () => _instance.Validate(InputStepBuilder.Build<int>());

            act.ShouldNotThrow();
        }

        private static IHasInput BuildHasInput()
        {
            var hasInput = new Mock<IHasInput>();

            hasInput
                .Setup(instance => instance.InputType)
                .Returns(typeof(int));

            return hasInput.Object;
        }
    }
}