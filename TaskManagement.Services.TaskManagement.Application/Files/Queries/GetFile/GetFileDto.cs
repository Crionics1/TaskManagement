using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Services.TaskManagement.Application.Files.Queries.GetFile
{
    public class GetFileDto
    {
        public Guid Id { get; set; }
        public byte[] Bytes { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
    }
}
