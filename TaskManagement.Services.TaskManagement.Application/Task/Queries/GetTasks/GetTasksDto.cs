using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Services.TaskManagement.Application.Task.Queries.GetTasks
{
    public class GetTasksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public List<Guid> FileIds { get; set; }
    }
}
