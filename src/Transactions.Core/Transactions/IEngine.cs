using System.Collections.Generic;
using Transactions.Core.Contract;

namespace Transactions.Core.Transactions
{
    public interface IEngine
    {
        object Current { get; set; }
        void Execute();
    }
}