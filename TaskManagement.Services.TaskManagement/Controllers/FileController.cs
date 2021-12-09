using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Application.Files.Queries.GetFile;

namespace TaskManagement.Services.TaskManagement.Controllers
{
    public class FileController : BaseController
    {
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var file = await Mediator.Send(new GetFileQuery { Id = id });

            return File(file.Bytes, "APPLICATION/octet-stream", file.Name);
        }
    }
}
