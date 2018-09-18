namespace Transactions.Core.Generic
{
    public interface IHasOutput<out TOutput> : IHasOutput
    {
        TOutput Output { get; }
    }
}