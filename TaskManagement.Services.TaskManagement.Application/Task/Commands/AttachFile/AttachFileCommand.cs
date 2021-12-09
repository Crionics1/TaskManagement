using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Exceptions;
using TaskManagement.Services.TaskManagement.Domain;
using TaskManagement.Services.TaskManagement.Domain.Entities;
using TaskManagement.Services.TaskManagement.Domain.Repositories;

namespace TaskManagement.Services.TaskManagement.Application.Task.Commands.AttachFile
{
    public class AttachFileCommand : IRequest<List<Guid>>
    {
        public int TaskId { get; set; }
        public ICollection<File> Files { get; set; }

        public class File
        {
            public string Name { get; set; }
            public string Extension { get; set; }
            public byte[] Bytes { get; set; }
        }

    }

    public class AttachFileCommandHandler : IRequestHandler<AttachFileCommand, List<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Domain.Entities.Task, int> _taskRepository;
        private readonly IRepository<File, Guid> _fileRepository;
        private readonly IRepository<TaskAttachment, int> _taskAttachmentRepository;

        public AttachFileCommandHandler(IUnitOfWork unitOfWork,
            IRepository<TaskManagement.Domain.Entities.Task, int> taskRepository,
            IRepository<File,Guid> fileRepository,
            IRepository<TaskAttachment,int> taskAttachmentRepository)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
            _fileRepository = fileRepository;
            _taskAttachmentRepository = taskAttachmentRepository;
        }

        public async Task<List<Guid>> Handle(AttachFileCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetAsync(request.TaskId);

            if (task == null)
            {
                throw new NotFoundException("Invalid Task Id");
            }

            var fileIds = new List<Guid>();

            foreach (var item in request.Files)
            {
                var file = new File
                {
                    Content = item.Bytes,
                    Extension = item.Name,
                    Name = item.Name,
                    Size = item.Bytes.Length
                };

                await _fileRepository.AddAsync(file);

                var taskAttachment = new TaskAttachment
                {
                    FileId = file.Id,
                    TaskId = task.Id
                };

                await _taskAttachmentRepository.AddAsync(taskAttachment);

                fileIds.Add(file.Id);
            }

            return fileIds;
        }
    }
}
