using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Exceptions;
using TaskManagement.Services.TaskManagement.Domain;
using TaskManagement.Services.TaskManagement.Domain.Entities;
using TaskManagement.Services.TaskManagement.Domain.Repositories;

namespace TaskManagement.Services.TaskManagement.Application.Task.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<int>
    {
        [Required]
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid? UserId { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TaskManagement.Domain.Entities.Task, int> _taskRepository;
        private readonly IRepository<ApplicationUser, Guid> _applicationRepository;
        private readonly IRepository<File, Guid> _fileRepository;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<TaskManagement.Domain.Entities.Task, int> taskRepository,
            IRepository<ApplicationUser,Guid> applicationRepository,
            IRepository<File,Guid> fileRepository)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
            _applicationRepository = applicationRepository;
            _fileRepository = fileRepository;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _applicationRepository.GetAsync(request.UserId.Value) != null;
            if (!userExists)
            {
                throw new NotFoundException("Invalid User Id");
            }

            var entity = new TaskManagement.Domain.Entities.Task
            {
                Title = request.Title,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                UserId = request.UserId.Value
            };

            await _taskRepository.AddAsync(entity);

            return entity.Id;
        }
    }

}
