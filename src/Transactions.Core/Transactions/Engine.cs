using System;
using System.Collections.Generic;
using Transactions.Core.Contract;

namespace Transactions.Core.Transactions
{
    public class Engine : IEngine
    {
        private readonly IList<ITransactionEntity> _entities;

        public object Current { get; set; }

        public Engine(IList<ITransactionEntity> entities)
        {
            _entities = entities;
        }

        public void Execute()
        {
            foreach (ITransactionEntity entity in _entities)
            {
                try
                {
                    ExecuteEntity(entity);
                }
                catch (Exception)
                {
                    RollbackExecutedEntities(entity);
                    throw;
                }
            }
        }

        private void ExecuteEntity(ITransactionEntity entity)
        {
            var beforeExecution = entity as IBeforeExecution;
            beforeExecution?.BeforeExecution();

            var inputExecute = entity as IInputExecute;
            inputExecute?.Execute(Current);

            var outputExecute = entity as IOutputExecute;
            Current = outputExecute?.Execute() ?? Current;

            var ioExecute = entity as IIOExecute;
            Current = ioExecute?.Execute(Current) ?? Current;

            var voidExecute = entity as IExecute;
            voidExecute?.Execute();

            var afterExecution = entity as IAfterExecution;
            afterExecution?.AfterExecution();
        }

        private void RollbackExecutedEntities(ITransactionEntity failed)
        {
            int failedIndex = _entities.IndexOf(failed);

            for (int index = failedIndex; index >= 0; index--)
            {
                IRollback foundStep = _entities[index] as IRollback;
                foundStep?.Rollback();
            }
        }
    }
}