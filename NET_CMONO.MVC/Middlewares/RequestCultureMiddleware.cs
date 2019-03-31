using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NET_CMONO.MVC.Middlewares.Options;

namespace NET_CMONO.MVC.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate next;

        private readonly RequestCultureOptions options;

        public RequestCultureMiddleware(RequestDelegate next, RequestCultureOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public Task Invoke(HttpContext context)
        {
            CultureInfo requestCulture = null;

            var cultureQuery = context.Request.Query["culture"];

            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                requestCulture = new CultureInfo(cultureQuery);
            }
            else
            {
                requestCulture = this.options.DefaultCulture;
            }

            if (requestCulture != null)
            {
#if !DNXCORE50
                Thread.CurrentThread.CurrentCulture = requestCulture;
                Thread.CurrentThread.CurrentUICulture = requestCulture;
#else
        CultureInfo.CurrentCulture = requestCulture;
        CultureInfo.CurrentUICulture = requestCulture;
#endif
            }

            // 调用管道中的下一个代理/中间件
            return this.next(context);
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseRequestCulture(new RequestCultureOptions());
        }

        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder, RequestCultureOptions options)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>(options);
        }
    }
}