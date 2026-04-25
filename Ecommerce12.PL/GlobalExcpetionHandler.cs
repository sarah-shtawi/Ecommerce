using Ecommerce12.DAL.DTO_s.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce12.PL
{
    public class GlobalExcpetionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorDetails = new ErrorHandling
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "server error",
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(errorDetails);
            return true;

        }
    }
}
