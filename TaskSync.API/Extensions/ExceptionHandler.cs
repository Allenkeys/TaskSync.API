using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.API.Extensions
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeatures != null)
                    {
                        switch (contextFeatures.Error)
                        {
                            case InvalidOperationException:
                            case InvalidDataException:
                            case KeyNotFoundException:
                            case ArgumentException:
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                break;
                            case DbUpdateException:
                                context.Response.StatusCode = StatusCodes.Status409Conflict;
                                break;
                            case UnauthorizedAccessException:
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                break;
                            default:
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                break;
                        }
                        string errMessage = context.Response.StatusCode == StatusCodes.Status500InternalServerError ?
                            "Internal server error, please contact support" : contextFeatures.Error.Message;
                        await context.Response.WriteAsync(new ErrorResponse
                        {
                            Message = errMessage,
                            Success = false,
                            Status = context.Response.StatusCode
                        }.ToString());
                    }
                });
            });
        }
    }
}
