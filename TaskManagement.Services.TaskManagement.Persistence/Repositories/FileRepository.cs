using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Files.Queries;
using TaskManagement.Services.TaskManagement.Application.Files.Queries.GetFile;

namespace TaskManagement.Services.TaskManagement.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly TaskContext _context;

        public FileRepository(TaskContext context)
        {
            _context = context;
        }

        public Task<GetFileDto> GetFileAsync(Guid id)
        {
            return _context.Files.Where(x => x.Id == id)
                .Select(x => new GetFileDto
                {
                    Id = x.Id,
                    Bytes = x.Content,
                    Name = x.Name,
                    Size = x.Size
                })
                .FirstOrDefaultAsync();
        }
    }
}
