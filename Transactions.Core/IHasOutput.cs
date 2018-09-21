using System;

namespace Transactions.Core
{
    public interface IHasOutput
    {
        Type OutputType { get; }
        object GetOutput();
    }
}