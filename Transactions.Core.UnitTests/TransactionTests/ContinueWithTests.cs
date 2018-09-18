using System;
using Shouldly;
using Transactions.Core.Exceptions;
using Xunit;

namespace Transactions.Core.UnitTests.TransactionTests
{
    public class ContinueWithTests : BaseTransactionStepTests
    {
        private readonly Transaction _instance;

        public ContinueWithTests()
        {
            _instance = new Transaction();
        }

        [Fact]
        public void ContinueWith_Throws_WhenFirstStepHasInput()
        {
            Action act = () => _instance.ContinueWith(InputStepBuilder.Build<int>());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void ContinueWith_DoesNotThrow_WhenFirstStepDoesNotHaveInput()
        {
            Action act = () => _instance.ContinueWith(StepBuilder.Build());

            act.ShouldNotThrow();
        }

        [Fact]
        public void ContinueWith_DoesNotThorw_WhenFirstStepHasOnlyOutput()
        {
            Action act = () => _instance.ContinueWith(OutputStepBuilder.Build<int>());

            act.ShouldNotThrow();
        }

        [Fact]
        public void ContinueWith_Throws_WhenStepInputDiffersFromPreviousStepOutput()
        {
            _instance.ContinueWith(OutputStepBuilder.Build<int>());

            Action act = () => _instance.ContinueWith(InputStepBuilder.Build<float>());

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void ContinueWith_DoesNotThrow_WhenStepInputIsTheSameWithPreviousStepOutput()
        {
            _instance.ContinueWith(OutputStepBuilder.Build<int>());

            Action act = () => _instance.ContinueWith(InputStepBuilder.Build<int>());

            act.ShouldNotThrow();
        }

        [Fact]
        public void ContinueWith_Throws_WhenStepHasInputButLastStepDoesNotHaveOutput()
        {
            _instance.ContinueWith(StepBuilder.Build());

            Action act = () => _instance.ContinueWith(InputStepBuilder.Build<int>());

            act.ShouldThrow<WrongSuccessionException>();
        }
    }
}