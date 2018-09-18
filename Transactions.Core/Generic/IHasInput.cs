namespace Transactions.Core.Generic
{
    public interface IHasInput<in TInput> : IHasInput
    {
        void Initialize(TInput input);
    }
}