using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Services.TaskManagement.Application.Task.Queries.GetTasks
{
    public class GetTasksQuery : IRequest<List<GetTasksDto>>
    {
        public int Page { get; set; }
        public int ItemCount { get; set; }
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<GetTasksDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<GetTasksDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetPagedTasksAsync(request.Page, request.ItemCount);
        }
    }
}
