using Shouldly;
using Transactions.Core.Generic.Transactions;
using Xunit;

namespace Transactions.Core.UnitTests.TransactionTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithoutArguments()
        {
            var actual = Transaction.Create();

            actual.ShouldBeOfType<Transaction>();
        }

        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithTemplateParameters()
        {
            var actual = Transaction.Create<int, int>();

            actual.ShouldBeOfType<Transaction<int, int>>();
        }

        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithArguments()
        {
            var actual = Transaction.Create<int, int>(1);

            actual.ShouldBeOfType<Transaction<int, int>>();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenItIsCalledWithoutArguments()
        {
            var actual = Transaction.Create();

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenItIsCalledWithTemplateParameters()
        {
            var actual = Transaction.Create<int, int>();

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenItIsCalledWithArguments()
        {
            var actual = Transaction.Create<int, int>(1);

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_ReturnsDifferentInstances_WhenItIsCalledWithoutArguments()
        {
            var first = Transaction.Create();
            var second = Transaction.Create();

            second.ShouldNotBe(first);
        }

        [Fact]
        public void Create_ReturnsDifferentInstances_WhenItIsCalledWithTemplateParameters()
        {
            var first = Transaction.Create<int, int>();
            var second = Transaction.Create<int, int>();

            second.ShouldNotBe(first);
        }

        [Fact]
        public void Create_ReturnsDifferentInstances_WhenItIsCalledWithArgument()
        {
            var first = Transaction.Create<int, int>(1);
            var second = Transaction.Create<int, int>(1);

            second.ShouldNotBe(first);
        }
    }
}