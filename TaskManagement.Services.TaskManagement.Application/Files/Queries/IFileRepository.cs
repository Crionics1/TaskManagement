using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Files.Queries.GetFile;

namespace TaskManagement.Services.TaskManagement.Application.Files.Queries
{
    public interface IFileRepository
    {
        Task<GetFileDto> GetFileAsync(Guid id);
    }
}
