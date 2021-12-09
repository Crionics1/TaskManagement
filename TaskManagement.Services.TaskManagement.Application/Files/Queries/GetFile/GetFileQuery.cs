using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Exceptions;

namespace TaskManagement.Services.TaskManagement.Application.Files.Queries.GetFile
{
    public class GetFileQuery : IRequest<GetFileDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, GetFileDto>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<GetFileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileAsync(request.Id);

            if (file == null)
            {
                throw new NotFoundException("File not found!");
            }

            return file;
        }
    }
}
