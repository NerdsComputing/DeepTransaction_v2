namespace Transactions.Core.Validations.Generic
{
    public interface IValidation<in T>
    {
        void Validate(T target);
    }
}