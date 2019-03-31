using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NET_CMONO.Framework;

namespace NET_CMONO.MVC.Middlewares
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ILogger<RequestIPMiddleware> logger{ get;set; }

        public RequestIPMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestIPMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {            
            // logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress.ToString());
            _logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress.ToString());
            await _next.Invoke(context);
        }
    }

    public static class RequestIPExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}