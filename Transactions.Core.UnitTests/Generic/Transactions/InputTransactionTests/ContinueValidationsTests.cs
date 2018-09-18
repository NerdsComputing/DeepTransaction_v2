using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations.Generic;
using Transactions.Core.Validations.Generic.InputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.InputTransactionTests
{
    public class ContinueValidationsTests
    {
        private readonly MockInputTransaction _instance;

        public ContinueValidationsTests()
        {
            _instance = new MockInputTransaction();
        }

        [Fact]
        public void ContinueValidations_Contains_NoFirstInputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is NoFirstInputValidation);
        }

        [Fact]
        public void ContinueValidations_Contains_WrongFirstInputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is WrongFirstInputValidation);
        }

        [Fact]
        public void ContinueValidations_Contains_WrongInputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is WrongInputValidation);
        }

        [Fact]
        public void ContinueValidations_Contains_NoInputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is NoInputValidation);
        }

        [Fact]
        public void ContinueValidations_Contains_NoLastOutputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is NoLastOutputValidation);
        }

        private class MockInputTransaction : InputTransaction<int>
        {
            public IEnumerable<IValidation<ITransactionStep>> GetContinueValidations()
            {
                return ContinueValidations;
            }
        }
    }
}