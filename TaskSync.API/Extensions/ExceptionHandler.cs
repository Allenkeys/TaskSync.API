using Microsoft.AspNetCore.Diagnostics;
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
