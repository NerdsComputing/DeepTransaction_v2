using System;
using Moq;
using Shouldly;
using Transactions.Core.Contract;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions;
using Transactions.Core.Transactions.Generic.Validations;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Generic.Validations
{
    public class EmptyQueueValidationTests : ValidationBaseTests
    {
        private readonly IValidation _instance;

        public EmptyQueueValidationTests()
        {
            _instance = new EmptyQueue(Entities);
        }

        [Fact]
        public void Validate_Throws_WhenCollectionIsEmpty()
        {
            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownException_HasMessage()
        {
            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenCollectionIsNotEmpty()
        {
            Entities.Add(new MockTransactionStep());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }
    }
}