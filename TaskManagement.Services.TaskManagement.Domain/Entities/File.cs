using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Services.TaskManagement.Domain.Entities
{
    public class File : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public byte[] Content { get; set; }
    }
}
