using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Task;
using TaskManagement.Services.TaskManagement.Application.Task.Queries.GetTasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Services.TaskManagement.Domain.Entities;

namespace TaskManagement.Services.TaskManagement.Persistence.Repositories
{
    public class TaskRepository : Repository<Domain.Entities.Task, int> ,ITaskRepository
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GetTasksDto>> GetPagedTasksAsync(int page, int count)
        {
            var skip = page * count;

            var dtos = await (from task in _context.Tasks
                       join user in _context.Users on task.UserId equals user.Id
                       let fileIds = _context.TaskAttachments.Where(x => x.TaskId == task.Id).Select(f => f.FileId).ToList()
                       orderby task.Id
                       select new GetTasksDto
                       {
                           Id = task.Id,
                           Description = task.Description,
                           ShortDescription = task.ShortDescription,
                           Title = task.Title,
                           UserName = user.UserName,
                           FileIds = fileIds
                       })
                       .OrderByDescending(x => x.Id)
                       .Skip(skip)
                       .Take(count)
                       .ToListAsync();

            return dtos;
        }

        public Task<List<TaskAttachment>> GetTaskAttachmentsAsync(int taskId)
        {
            return _context.TaskAttachments.Where(x => x.TaskId == taskId)
                .ToListAsync();
        }
    }
}
