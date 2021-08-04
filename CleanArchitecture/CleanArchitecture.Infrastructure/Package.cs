using System;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Repositories;
using CleanArchitecture.CrossCutting.Options;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Models;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class Package
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var schoolOptions = configuration.GetOptions<SchoolOptions>();
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(schoolOptions.ConnectionString, builder => builder.EnableRetryOnFailure()));
            
            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<SchoolContext>());
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
        }
    }
}