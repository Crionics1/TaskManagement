using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.InMemory;
using TaskManagement.Services.TaskManagement.Domain;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Services.TaskManagement.Domain.Repositories;
using TaskManagement.Services.TaskManagement.Persistence.Repositories;
using TaskManagement.Services.TaskManagement.Application.Task;
using TaskManagement.Services.TaskManagement.Application.Files.Queries;

namespace TaskManagement.Services.TaskManagement.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<TaskContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }

    }
}
