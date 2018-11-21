using System.Collections.Generic;
using Transactions.Core.Contract;

namespace Transactions.Core.UnitTests.Transactions
{
    public abstract class ValidationBaseTests
    {
        protected readonly ICollection<ITransactionEntity> Entities;

        protected ValidationBaseTests()
        {
            Entities = new List<ITransactionEntity>();
        }
    }
}