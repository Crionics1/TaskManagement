using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Services.TaskManagement.Domain
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task StartAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
