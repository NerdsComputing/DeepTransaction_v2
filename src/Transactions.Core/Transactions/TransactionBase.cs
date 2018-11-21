using System;
using System.Collections.Generic;
using Transactions.Core.Contract;

namespace Transactions.Core.Transactions
{
    public abstract class TransactionBase : IBeforeExecution, IAfterExecution, IRollback
    {
        internal readonly IList<ITransactionEntity> Entities;

        private protected abstract IEngine Engine { get; }

        protected TransactionBase()
        {
            Entities = new List<ITransactionEntity>();
        }

        public virtual void BeforeExecution()
        {
        }

        public virtual void AfterExecution()
        {
        }

        public virtual void Rollback()
        {
        }

        private protected void BaseExecute()
        {
            BeforeExecution();
            ExecuteEntities();
            AfterExecution();
        }

        private void ExecuteEntities()
        {
            try
            {
                Engine.Execute();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }
    }
}