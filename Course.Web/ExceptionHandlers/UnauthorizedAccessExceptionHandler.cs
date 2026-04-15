using Microsoft.AspNetCore.Diagnostics;

namespace Course.Web.ExceptionHandlers
{
    public class UnauthorizedAccessExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is UnauthorizedAccessException)
            {
                httpContext.Response.Redirect("/Auth/SignIn");
                return new ValueTask<bool>(true);
            }
            return new ValueTask<bool>(false);
        }
    }
}
