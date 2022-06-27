namespace GlobalErrorHandling.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Net;

    public class ErrorHandlerFilterAttribute : ExceptionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An error occurred trying to process the request.",
                Detail = context.Exception.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Instance = context.HttpContext.Request.Path
            });

            context.ExceptionHandled = true;
        }
    }
}