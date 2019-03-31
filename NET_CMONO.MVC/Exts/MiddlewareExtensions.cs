using Microsoft.AspNetCore.Builder;
using NET_CMONO.MVC.Middlewares;

namespace NET_CMONO.MVC.Exts
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTime(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseTimeMiddleware>();
        }

        // public static IApplicationBuilder UseRequestKey(this IApplicationBuilder builder)
        // {
        //     return builder.UseMiddleware<RequestKeyMiddleware>();
        // }

        // public static IApplicationBuilder UseAuthorizationOperation(this IApplicationBuilder builder)
        // {
        //     return builder.UseMiddleware<AuthorizationOperationMiddleware>();
        // }

    }
}