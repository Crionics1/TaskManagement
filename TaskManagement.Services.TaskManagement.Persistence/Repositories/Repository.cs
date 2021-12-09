using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Domain;
using TaskManagement.Services.TaskManagement.Domain.Repositories;

namespace TaskManagement.Services.TaskManagement.Persistence.Repositories
{
    public class Repository<T, T2> : IRepository<T, T2> where T : class, IEntity<T2>
    {
        private readonly TaskContext _context;

        public Repository(TaskContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetAsync(T2 id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<T> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
