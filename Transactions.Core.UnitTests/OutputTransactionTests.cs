using Shouldly;
using Transactions.Core.Generic.Transactions;
using Xunit;

namespace Transactions.Core.UnitTests
{
    public class OutputTransactionTests
    {
        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithoutArguments()
        {
            var actual = OutputTransaction.Create<int>();

            actual.ShouldBeOfType<OutputTransaction<int>>();
        }

        [Fact]
        public void Create_ReturnsRightType_WhenItIsCalledWithArguments()
        {
            var actual = OutputTransaction.Create(1);

            actual.ShouldBeOfType<OutputTransaction<int>>();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenItIsCalledWithoutArguments()
        {
            var actual = OutputTransaction.Create<int>();

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_DoesNotReturnNull_WhenItIsCalledWithArguments()
        {
            var actual = OutputTransaction.Create(1);

            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Create_ReturnsDifferentInstances_WhenItIsCalledWithoutArguments()
        {
            var first = OutputTransaction.Create<int>();
            var second = OutputTransaction.Create<int>();

            second.ShouldNotBe(first);
        }
        
        [Fact]
        public void Create_ReturnsDifferentInstances_WhenItIsCalledWithArguments()
        {
            var first = OutputTransaction.Create(1);
            var second = OutputTransaction.Create(1);
            
            second.ShouldNotBe(first);
        }
    }
}