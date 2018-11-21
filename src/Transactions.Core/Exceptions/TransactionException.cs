using System;

namespace Transactions.Core.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException()
        {
        }

        protected TransactionException(string message) : base(message)
        {
        }
    }
}