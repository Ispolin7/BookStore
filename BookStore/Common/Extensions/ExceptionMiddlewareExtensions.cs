using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BookStore.Common.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                var result = new ErrorDetails
                {
                    StatusCode = 400,
                    Message = exception.Message,
                    //Source = exception.Source,
                    //StackTrace = exception.StackTrace,
                    //InnerException = exception.InnerException.Message
                }.ToString();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(result);
            }));
        }
    }
}
