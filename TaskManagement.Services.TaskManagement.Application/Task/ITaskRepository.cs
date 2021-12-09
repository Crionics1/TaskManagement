using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Task.Queries.GetTasks;
using TaskManagement.Services.TaskManagement.Domain.Entities;
using TaskManagement.Services.TaskManagement.Domain.Repositories;

namespace TaskManagement.Services.TaskManagement.Application.Task
{
    public interface ITaskRepository : IRepository<TaskManagement.Domain.Entities.Task,int>
    {
        Task<List<TaskAttachment>> GetTaskAttachmentsAsync(int taskId);

        Task<List<GetTasksDto>> GetPagedTasksAsync(int page, int count);
    }
}
