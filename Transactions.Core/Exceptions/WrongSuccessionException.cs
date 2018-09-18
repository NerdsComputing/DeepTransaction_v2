namespace Transactions.Core.Exceptions
{
    public class WrongSuccessionException : TransactionException
    {
        public WrongSuccessionException(string message) : base(message)
        {
        }
    }
}