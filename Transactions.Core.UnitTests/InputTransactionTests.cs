using Shouldly;
using Transactions.Core.Generic.Transactions;
using Xunit;

namespace Transactions.Core.UnitTests
{
    public class InputTransactionTests
    {
        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithoutArguments()
        {
            var actual = InputTransaction.Create<int>();

            actual.ShouldBeOfType<InputTransaction<int>>();
        }

        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithArguments()
        {
            var actual = InputTransaction.Create(1);

            actual.ShouldBeOfType<InputTransaction<int>>();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenNoArgumentsArePassed()
        {
            var actual = InputTransaction.Create<int>();

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenArgumentsArePassed()
        {
            var actual = InputTransaction.Create(3);

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_ReturnsDifferentInstances_WhenItIsCalledWithoutArguments()
        {
            var first = InputTransaction.Create<int>();
            var second = InputTransaction.Create<int>();

            second.ShouldNotBe(first);
        }

        [Fact]
        public void Create_ReturnsDifferenceInstances_WhenItIsCalledWithArguments()
        {
            var first = InputTransaction.Create(1);
            var second = InputTransaction.Create(2);

            second.ShouldNotBe(first);
        }
    }
}