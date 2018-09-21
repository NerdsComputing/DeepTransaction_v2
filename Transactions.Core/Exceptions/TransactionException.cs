using System;

namespace Transactions.Core.Exceptions
{
    public class TransactionException : Exception
    {
        protected TransactionException(string message) : base(message)
        {
        }
    }
}