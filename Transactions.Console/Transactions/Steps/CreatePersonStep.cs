using Transactions.Console.Models;
using Transactions.Core.Generic;
using Transactions.Core.Generic.Steps;

namespace Transactions.Console.Transactions.Steps
{
    public class CreatePersonStep : TransactionStep<string, Person>
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _personId;
        private Person _createdPerson;

        public override Person Output => _createdPerson;

        public CreatePersonStep(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void Initialize(string input)
        {
            _personId = input;
        }

        public override void BeforeExecution()
        {
            _unitOfWork.BeginTransaction();
        }

        public override void Execute()
        {
            _createdPerson = new Person
            {
                Id = _personId,
            };
        }

        public override void AfterExecution()
        {
            _unitOfWork.Commit();
        }

        public override void Rollback()
        {
            _unitOfWork.Rollback();
        }
    }
}