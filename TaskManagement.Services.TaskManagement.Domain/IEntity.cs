using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Services.TaskManagement.Domain
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
