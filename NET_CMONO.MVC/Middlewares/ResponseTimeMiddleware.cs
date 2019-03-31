using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NET_CMONO.MVC.Middlewares
{
    /// <summary>
    /// 响应时间的中间件
    /// </summary>
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("ResponseTimeMiddleware...");
            await _next.Invoke(context);
            sw.Stop();
            Console.WriteLine($"页面响应时间为：{sw.ElapsedMilliseconds}ms");
        }

    }
}