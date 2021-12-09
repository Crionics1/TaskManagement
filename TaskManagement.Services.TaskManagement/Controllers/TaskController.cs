using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Task.Commands.AttachFile;
using TaskManagement.Services.TaskManagement.Application.Task.Commands.CreateTask;
using TaskManagement.Services.TaskManagement.Application.Task.Commands.DeleteTask;
using TaskManagement.Services.TaskManagement.Application.Task.Commands.UpdateTask;
using TaskManagement.Services.TaskManagement.Application.Task.Queries.GetTasks;

namespace TaskManagement.Services.TaskManagement.Controllers
{
    public class TaskController : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "CreateTask")]
        public async Task<ActionResult<int>> Post([FromBody] CreateTaskCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(id);
        }

        [HttpPost("attachment")]
        [Authorize(Roles = "UpdateTask")]
        public async Task<ActionResult<int>> UploadFile(List<IFormFile> files, int taskId)
        {
            var request = new AttachFileCommand() { TaskId = taskId };

            foreach (var formFile in files)
            {
                request.Files = new List<AttachFileCommand.File>();
                if (formFile.Length > 0)
                {
                    using (var readStream = formFile.OpenReadStream())
                    {
                        var length = readStream.Length;
                        var bytes = new byte[length];
                        await readStream.ReadAsync(bytes);

                        request.Files.Add(new AttachFileCommand.File
                        {
                            Bytes = bytes,
                            Name = formFile.FileName,
                            Extension = formFile.FileName
                        });
                    }
                }
            }

            return Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Authorize(Roles =  "UpdateTask")]
        public async Task<ActionResult<int>> Put([FromBody]UpdateTaskCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(id);
        }

        [HttpDelete("{TaskId}")]
        [Authorize(Roles = "DeleteTask")]
        public async Task<ActionResult> Delete([FromRoute]DeleteTaskCommand request)
        {
            await Mediator.Send(request);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "GetTask")]
        public async Task<ActionResult<List<GetTasksDto>>> Get([FromQuery] GetTasksQuery request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

    }
}
