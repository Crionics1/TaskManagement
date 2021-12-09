using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Domain;

namespace TaskManagement.Services.TaskManagement.Persistence
{
    internal class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly TaskContext _taskContext;
        private bool _commited = false;
        private IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        public async Task CommitAsync()
        {
            await _dbContextTransaction.CommitAsync();
            _commited = true;
        }


        public async Task RollbackAsync()
        {
            await _dbContextTransaction.RollbackAsync();
        }

        public async Task StartAsync()
        {
            _dbContextTransaction = await _taskContext.Database.BeginTransactionAsync();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_dbContextTransaction != null && !_commited)
            {
                await this.RollbackAsync();
            }
        }
    }
}
