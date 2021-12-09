using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Services.TaskManagement.Domain.Repositories
{
    public interface IRepository<T, T2> where T : IEntity<T2>
    {
        public Task<T> GetAsync(T2 id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<T> RemoveAsync(T entity);
    }
}
