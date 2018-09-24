namespace Transactions.Core.Contract.Generic
{
    public interface IInputExecute<in TInput> : IInputExecute
    {
        void Execute(TInput input);
    }
}