using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Services.TaskManagement.Domain.Entities
{
    public class TaskAttachment : IEntity<int>
    {
        public int Id { get; set; }
        public Guid FileId { get; set; }
        public int TaskId { get; set; }
    }
}
