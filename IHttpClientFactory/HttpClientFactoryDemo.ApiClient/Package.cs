using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HttpClientFactoryDemo.ApiClient
{
    public static class Package
    {
        public static IServiceCollection AddStarWarsApiClient(this IServiceCollection services, Action<HttpClient> options)
        {
            services.AddTransient<StarWarsMiddleware>();
            
            services.AddHttpClient<StarWarsApiClient>(options)
                .AddHttpMessageHandler<StarWarsMiddleware>().SetHandlerLifetime(TimeSpan.FromMinutes(10));

            return services;
        }
    }

    public class StarWarsMiddleware : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("my-custom-header", Guid.NewGuid().ToString());
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}