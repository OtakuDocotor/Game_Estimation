using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.ExceptionHandlers
{
    public class UnauthorizedExceptionHandler(IProblemDetailsService _problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            httpContext.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = "Authentication failed",
                Detail = exception.Message, 
                Instance = httpContext.Request.Path,
                Type = exception.GetType().Name
            };

            var problemDetailsContext = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
                Exception = exception
            };

            await _problemDetailsService.WriteAsync(problemDetailsContext);
            return true;
        }
    }
}
