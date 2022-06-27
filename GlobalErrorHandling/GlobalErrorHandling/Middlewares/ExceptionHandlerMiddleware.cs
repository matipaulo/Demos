namespace GlobalErrorHandling.Middlewares
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class ExceptionHandlerMiddleware : IMiddleware
    {
        /// <inheritdoc />
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An error occurred trying to process the request.",
                Detail = exception.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Instance = httpContext.Request.Path
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}