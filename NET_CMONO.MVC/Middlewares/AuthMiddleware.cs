using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NET_CMONO.MVC.Models;

namespace NET_CMONO.MVC.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AuthMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestIPMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            // if (context.Session.GetString("CurrentUser") == null)
            // {

            // }
            // logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress.ToString());
            _logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress.ToString());
            await _next.Invoke(context);
        }


        /// <summary>
        /// not authorized request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnNoAuthorized(HttpContext context)
        {
            BaseResponseResultModel response = new BaseResponseResultModel
            {
                Code = "401",
                Message = "You are not authorized!"
            };
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("weidenglu");
        }

    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}