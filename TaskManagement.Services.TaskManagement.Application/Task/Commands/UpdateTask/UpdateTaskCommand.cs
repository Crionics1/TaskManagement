using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Exceptions;
using TaskManagement.Services.TaskManagement.Domain.Repositories;

namespace TaskManagement.Services.TaskManagement.Application.Task.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<int>
    {
        [Required]
        public int? TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid? UserId { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, int>
    {
        private readonly Domain.Repositories.IRepository<Domain.Entities.Task, int> _repository;
        private readonly IRepository<Domain.Entities.ApplicationUser, Guid> _userRepository;

        public UpdateTaskCommandHandler(IRepository<TaskManagement.Domain.Entities.Task, int> repository,
            IRepository<TaskManagement.Domain.Entities.ApplicationUser, Guid> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetAsync(request.TaskId.Value);

            if (task == null)
            {
                throw new NotFoundException("Task not found");
            }

            var user = await _userRepository.GetAsync(request.UserId.Value);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }


            task.Title = request.Title;
            task.Description = request.Description;
            task.ShortDescription = request.ShortDescription;
            task.UserId = request.UserId.Value;

            await _repository.UpdateAsync(task);

            return task.Id;
        }
    }
}
