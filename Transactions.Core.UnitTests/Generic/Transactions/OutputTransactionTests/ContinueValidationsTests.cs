using System.Collections.Generic;
using Shouldly;
using Transactions.Core.Generic.Transactions;
using Transactions.Core.Validations.Generic;
using Transactions.Core.Validations.Generic.OutputTransaction.ContinueWith;
using Xunit;

namespace Transactions.Core.UnitTests.Generic.Transactions.OutputTransactionTests
{
    public class ContinueValidationsTests
    {
        private readonly MockOutputTransaction _instance;

        public ContinueValidationsTests()
        {
            _instance = new MockOutputTransaction();
        }

        [Fact]
        public void ContinueValidations_Contains_FirstInputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is FirstInputValidation);
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

        [Fact]
        public void ContinueValidations_Contains_InputValidation()
        {
            IEnumerable<IValidation<ITransactionStep>> validations = _instance.GetContinueValidations();

            validations.ShouldContain(instance => instance is InputValidation);
        }

        private class MockOutputTransaction : OutputTransaction<int>
        {
            public IEnumerable<IValidation<ITransactionStep>> GetContinueValidations()
            {
                return ContinueValidations;
            }
        }
    }
}