using CQRS.Core.Data;
using CQRS.Core.Extensions;
using CQRS.Core.Models;
using CQRS.Core.Repositories;
using CQRS.Core.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Core
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