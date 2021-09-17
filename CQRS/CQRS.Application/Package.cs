using CQRS.Application.Behaviors;
using CQRS.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CQRS.Application
{
    public static class Package
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateStudentCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        }
    }
}