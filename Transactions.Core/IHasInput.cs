using System;

namespace Transactions.Core
{
    public interface IHasInput
    {
        Type InputType { get; }
        void Initialize(object input);
    }
}