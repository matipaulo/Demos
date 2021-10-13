using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Playground.Api.Data;
using Playground.Api.GraphQL.Mutations;
using Playground.Api.GraphQL.Queries;
using Playground.Api.GraphQL.Resolvers;
using Playground.Api.GraphQL.Types;

namespace Playground.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<LibraryContext>(context => context.UseInMemoryDatabase("LibraryDB"));

            services.AddLogging(builder => builder.AddConsole());

            services.AddScoped<AuthorResolver>();
            
            services
                .AddGraphQLServer()
                .AddType<BookType>()
                .AddType<AuthorType>()
                .AddMutationType<Mutation>()
                .AddQueryType<Query>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UsePlayground();
            }
            
            app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
        }
    }
}