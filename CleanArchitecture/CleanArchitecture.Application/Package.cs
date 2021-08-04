using System.Reflection;
using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class Package
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateStudentCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            // services.AddLogging(options => options.AddConsole());
        }
    }
}