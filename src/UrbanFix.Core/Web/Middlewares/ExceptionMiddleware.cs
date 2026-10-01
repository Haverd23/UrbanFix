using Microsoft.AspNetCore.Http;
using UrbanFix.Core.Domain;
using UrbanFix.Core.Web.ApiResponse;
namespace UrbanFix.Core.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(DomainException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail(ex.Message));
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail(ex.Message));
            }
        }
    }
}
