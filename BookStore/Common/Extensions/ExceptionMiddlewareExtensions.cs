using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;

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
                string result;

                if (exception is ValidationException errors)
                {
                    result = JsonConvert.SerializeObject(new
                    {
                        Message = "Validation error",
                        Result = errors.State.ToDictionary(e => e.Key, e => e.Value)
                    });
                }
                else
                {
                    result = new ErrorDetails(
                        400,
                        exception.Message,
                        exception.Source ?? null,
                        exception.StackTrace ?? null,
                        exception.InnerException.Message ?? null
                    ).ToString();
                }


                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(result);
            }));
        }
    }
}
