using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Exceptions;
using TaskManagement.Services.TaskManagement.Domain.Repositories;

namespace TaskManagement.Services.TaskManagement.Application.Task.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        [Required]
        public int? TaskId { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _repository;
        private readonly IRepository<Domain.Entities.TaskAttachment, int> _taskAttachmentRepository;
        private readonly IRepository<Domain.Entities.File, Guid> _fileRepository;

        public DeleteTaskCommandHandler(ITaskRepository repository,
            IRepository<TaskManagement.Domain.Entities.TaskAttachment, int> taskAttachmentRepository,
            IRepository<TaskManagement.Domain.Entities.File, Guid> fileRepository)
        {
            _repository = repository;
            _taskAttachmentRepository = taskAttachmentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(request.TaskId.Value);

            if (entity == null)
            {
                throw new NotFoundException("Task not Found");
            }

            var taskAttachments = await _repository.GetTaskAttachmentsAsync(entity.Id);

            foreach (var item in taskAttachments)
            {
                await _taskAttachmentRepository.RemoveAsync(item);
                
                //probably better to delete without loading into memory
                var file = await _fileRepository.GetAsync(item.FileId);
                await _fileRepository.RemoveAsync(file);
            }


            await _repository.RemoveAsync(entity);

            return Unit.Value;
        }
    }
}
