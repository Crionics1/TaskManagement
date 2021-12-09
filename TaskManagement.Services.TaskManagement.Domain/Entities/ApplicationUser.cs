using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Services.TaskManagement.Domain;

namespace TaskManagement.Services.TaskManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {

    }
}
