namespace Transactions.Core.Contract.Generic
{
    public interface IExecute<in TInput, out TOutput> : IIOExecute
    {
        TOutput Execute(TInput input);
    }
}