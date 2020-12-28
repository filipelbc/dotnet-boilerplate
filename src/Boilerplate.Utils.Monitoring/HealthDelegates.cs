using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Boilerplate.Utils.Monitoring
{
    public static class HealthDelegates
    {
        public static Func<HttpContext, HealthReport, Task> BuildHealthResponseWriter(JsonSerializerOptions jsonOptions)
        {
            return (HttpContext context, HealthReport result) =>
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                return context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
            };
        }
    }
}
